using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLPrivateServer.Packets
{
	public class PacketReader : Packet
	{
		private byte[] _bytes = default(byte[]);
		private List<byte> bytes = new List<byte>();
		private bool invalidated = false;
		public override int Length => Bytes.Length;
		public int ReadPosition { get; set; }

		public override byte[] Bytes
		{
			get
			{
				if (_bytes == null || invalidated)
				{
					_bytes = PacketDecoding(bytes.ToArray());

					invalidated = false;
				}

				return _bytes;
			}
		}

		public PacketReader()
		{
		}

		public PacketReader(IEnumerable<byte> bytes)
		{
			this.bytes.AddRange(bytes);

			invalidated = true;
		}

		public byte ReadByte()
		{
			ThrowOnEnd();

			return Bytes[ReadPosition++];
		}

		public byte[] ReadBytes(int length)
		{
			ThrowOnEnd();

			var ret = Bytes.Skip(ReadPosition).Take(length).ToArray();

			ReadPosition += length;

			return ret;
		}

		public sbyte ReadSByte()
		{
			ThrowOnEnd();

			return (sbyte)ReadByte();
		}

		public short ReadShort()
		{
			ThrowOnEnd();

			var ret = BitConverter.ToInt16(Bytes, ReadPosition);

			ReadPosition += 2;

			return ret;
		}

		public ushort ReadUShort()
		{
			ThrowOnEnd();

			var ret = BitConverter.ToUInt16(Bytes, ReadPosition);

			ReadPosition += 2;

			return ret;
		}

		public int ReadInt()
		{
			ThrowOnEnd();

			var ret = BitConverter.ToInt32(Bytes, ReadPosition);

			ReadPosition += 4;

			return ret;
		}

		public uint ReadUInt()
		{
			ThrowOnEnd();

			var ret = BitConverter.ToUInt32(Bytes, ReadPosition);

			ReadPosition += 4;

			return ret;
		}

		public long ReadLong()
		{
			ThrowOnEnd();

			var ret = BitConverter.ToInt64(Bytes, ReadPosition);

			ReadPosition += 8;

			return ret;
		}

		public ulong ReadULong()
		{
			ThrowOnEnd();

			var ret = BitConverter.ToUInt64(Bytes, ReadPosition);

			ReadPosition += 8;

			return ret;
		}

		public double ReadDouble()
		{
			ThrowOnEnd();

			var ret = BitConverter.ToDouble(Bytes, ReadPosition);

			ReadPosition += sizeof(double);

			return ret;
		}

		public float ReadSingle()
		{
			ThrowOnEnd();

			var ret = BitConverter.ToSingle(Bytes, ReadPosition);

			ReadPosition += sizeof(float);

			return ret;
		}

		public string ReadString(int length)
		{
			ThrowOnEnd();

			var ret = (DefaultEncoding ?? Encoding.ASCII).GetString(Bytes, ReadPosition, length);

			ReadPosition += length;

			return ret;
		}

		private void ThrowOnEnd()
		{
			if (Bytes.Length <= ReadPosition)
				throw new InvalidOperationException("Cannot read past the end of the packet.");
		}
	}
}