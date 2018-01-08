﻿using WLPrivateServer.PacketHandler;
using WLPrivateServer.Packets;
using WLPrivateServer.Sockets;
using WLPrivateServer.Users;

namespace WLPrivateServer.Login.Handlers
{
	[Handles(0)]
	public class ActionCode_0 : IHandle
	{
		public void Handle(User user, PacketReader packet)
		{
			throw new System.NotImplementedException();
		}

		public void Handle(Socket socket, PacketReader packet)
		{
			EnqueuePacket_1_9(socket);

			EnqueuePacket_37_1(socket);
		}

		private static void EnqueuePacket_37_1(Socket socket)
		{
			var writer = Packet.WriterFactoryAdapter();

			writer.WriteBytes(new byte[] {037, 001, 145, 001, 002, 101, 000,
				002, 102, 000, 002, 103, 000, 002, 106, 000, 002, 202,
				000, 002, 201, 000, 002, 204, 000, 002, 203, 000, 002,
				045, 001, 002, 047, 001, 001, 105, 000, 002, 046, 001,
				001, 146, 001, 001, 104, 000, 002, 107, 000, 002, 148,
				001, 001, 147, 001, 001, 245, 001, 002, 246, 001, 001,
				247, 001, 001, 234, 003, 001, 235, 003, 001, 078, 004,
				001, 079, 004, 001, 035, 003, 001, 033, 003, 002, 034,
				003, 001, 233, 003, 002, 133, 003, 001, 135, 003, 001,
				134, 003, 001, 077, 004, 002});

			socket.Enqueue(writer);
		}

		private static void EnqueuePacket_1_9(Socket socket)
		{
			var writer = Packet.WriterFactoryAdapter();

			writer.WriteByte(1);
			writer.WriteByte(9);
			writer.WriteString("PrivateServer");
			writer.WriteByte(0);

			socket.Enqueue(writer);
		}
	}
}