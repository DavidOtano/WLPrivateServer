using System;
using System.Collections.Generic;
using System.Linq;
using WLPrivateServer.Packets;

namespace WLPrivateServer.Sockets
{
	/// <summary>
	/// This class implements a nonblocking TCP I/O Socket
	/// </summary>
	public class Socket : IDisposable
	{
		private readonly System.Net.Sockets.Socket socket;

		private readonly object readSyncRoot = new object();
		private readonly object writeSyncRoot = new object();

		private List<byte> readBuffer = new List<byte>();
		private List<byte> writeBuffer = new List<byte>();

		public bool TransmissionClosed { get; private set; } = false;
		private bool closed = false;

		public Socket(System.Net.Sockets.Socket socket)
		{
			this.socket = socket;
			this.socket.Blocking = false;
		}

		public void Read()
		{
			var buffer = new byte[8192];

			try
			{
				var length = socket.Receive(buffer);

				if (length == 0)
					throw new SocketClosedException();

				lock (readSyncRoot)
				{
					readBuffer.AddRange(buffer.Take(length));
				}
			}
			catch (System.Net.Sockets.SocketException ex)
			{
				if (ex.SocketErrorCode != System.Net.Sockets.SocketError.WouldBlock)
					throw new SocketClosedException();
			}
		}

		public void Write()
		{
			lock (writeSyncRoot)
			{
				if (writeBuffer.Count == 0)
				{
					if (TransmissionClosed && !closed)
						Close();

					return;
				}
			}

			try
			{
				int length = socket.Send(writeBuffer.ToArray());

				if (length == 0)
					throw new SocketClosedException();

				lock (writeSyncRoot)
				{
					writeBuffer = writeBuffer.Skip(length).ToList();
				}
			}
			catch (System.Net.Sockets.SocketException ex)
			{
				if (ex.SocketErrorCode != System.Net.Sockets.SocketError.WouldBlock)
					throw new SocketClosedException();
			}
		}

		public void Enqueue(object obj)
		{
			if (TransmissionClosed)
				return;

			PacketWriter writer = null;

			if (typeof(PacketWriter).IsAssignableFrom(obj.GetType()))
				writer = obj as PacketWriter;
			else
				writer = PacketMapper.Map<PacketWriter>(obj);

			if (writer.LastTransmission)
				TransmissionClosed = true;

			lock (writeSyncRoot)
			{
				writeBuffer.AddRange(writer.Bytes);
			}
		}

		public PacketReader Dequeue()
		{
			PacketReader reader = null;
			byte[] buff = null;

			lock (readSyncRoot)
			{
				if (readBuffer.Count < 4)
					return null;

				buff = Packet.PacketDecoding(readBuffer.Take(4).ToArray());
			}

			if (BitConverter.ToUInt16(buff, 0) != 0x44F4)
				throw new InvalidReceiveDataException();

			var length = BitConverter.ToUInt16(buff, 2);

			lock (readSyncRoot)
			{
				if (length <= readBuffer.Count)
				{
					buff = readBuffer.Take(4 + length).ToArray();

					readBuffer = readBuffer.Skip(4 + length).ToList();

					reader = new PacketReader(buff);
				}
			}

			return reader;
		}

		public void Close()
		{
			socket.Close();

			closed = true;
		}

		public void Dispose()
		{
			socket.Dispose();
		}
	}
}