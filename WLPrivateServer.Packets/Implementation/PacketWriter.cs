using System;
using System.Collections.Generic;
using System.Text;

namespace WLPrivateServer.Packets
{
	public class PacketWriter : Packet
	{
		private byte[] _bytes = null;
		private List<byte> bytes = new List<byte>();
		private bool invalidated = false;

		public override int Length => Bytes.Length;

		public bool LastTransmission { get; set; } = false;

		public override byte[] Bytes
		{
			get
			{
				if (_bytes == null || invalidated)
				{
					_bytes = PacketEncoding(BuildPacketHeader(bytes.ToArray()));

					invalidated = false;
				}

				return _bytes;
			}
		}

		public PacketWriter()
		{
		}

		public PacketWriter(IEnumerable<byte> bytes)
		{
			this.bytes.AddRange(bytes);

			invalidated = true;
		}

		public PacketWriter WriteByte(byte val)
		{
			bytes.Add(val);

			invalidated = true;

			return this;
		}

		public PacketWriter WriteSByte(sbyte val)
		{
			bytes.Add((byte)val);

			invalidated = true;

			return this;
		}

		public PacketWriter WriteBytes(IEnumerable<byte> bytes)
		{
			this.bytes.AddRange(bytes);

			invalidated = true;

			return this;
		}

		public PacketWriter WriteShort(short val)
		{
			WriteBytes(BitConverter.GetBytes(val));

			invalidated = true;

			return this;
		}

		public PacketWriter WriteUShort(ushort val)
		{
			WriteBytes(BitConverter.GetBytes(val));

			invalidated = true;

			return this;
		}

		public PacketWriter WriteInt(int val)
		{
			WriteBytes(BitConverter.GetBytes(val));

			invalidated = true;

			return this;
		}

		public PacketWriter WriteUInt(uint val)
		{
			WriteBytes(BitConverter.GetBytes(val));

			invalidated = true;

			return this;
		}

		public PacketWriter WriteLong(long val)
		{
			WriteBytes(BitConverter.GetBytes(val));

			invalidated = true;

			return this;
		}

		public PacketWriter WriteULong(ulong val)
		{
			WriteBytes(BitConverter.GetBytes(val));

			invalidated = true;

			return this;
		}

		public PacketWriter WriteDouble(double val)
		{
			WriteBytes(BitConverter.GetBytes(val));

			invalidated = true;

			return this;
		}

		public PacketWriter WriteSingle(float val)
		{
			WriteBytes(BitConverter.GetBytes(val));

			invalidated = true;

			return this;
		}

		public PacketWriter WriteString(string val)
		{
			WriteBytes((DefaultEncoding ?? Encoding.ASCII).GetBytes(val));

			invalidated = true;

			return this;
		}

		public PacketWriter WriteString(string val, int length)
		{
			WriteBytes((DefaultEncoding ?? Encoding.ASCII).GetBytes(val.Substring(0, length)));

			invalidated = true;

			return this;
		}

		public PacketWriter WriteString(string val, int index, int length)
		{
			WriteBytes((DefaultEncoding ?? Encoding.ASCII).GetBytes(val.Substring(index, length)));

			invalidated = true;

			return this;
		}
	}
}