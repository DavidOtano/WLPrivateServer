using System;
using System.Linq;
using System.Text;

namespace WLPrivateServer.Packets
{
	public abstract class Packet
	{
		public static Func<byte[], byte[]> PacketEncoding { get; set; } = x => x;
		public static Func<byte[], byte[]> PacketDecoding { get; set; } = x => x;
		public static Func<byte[], byte[]> BuildPacketHeader { get; set; } = x => Enumerable.Empty<byte>().ToArray();
		public static Encoding DefaultEncoding { get; set; } = Encoding.ASCII;
		public static Func<PacketReader> ReaderFactoryAdapter { get; set; } = () => new PacketReader();
		public static Func<PacketWriter> WriterFactoryAdapter { get; set; } = () => new PacketWriter();

		public abstract int Length { get; }
		public abstract byte[] Bytes { get; }
	}
}