using System;
using WLPrivateServer.Login.Implementations;
using WLPrivateServer.PacketHandler;
using WLPrivateServer.Packets;
using WLPrivateServer.Repository;
using WLPrivateServer.Sockets;
using WLPrivateServer.Users;

namespace WLPrivateServer.Login.Handlers
{
	[Handles(63)]
	public class ActionCode_63 : IHandle
	{
		public void Handle(User user, PacketReader packet)
		{
			throw new NotImplementedException();
		}

		public void Handle(Socket socket, PacketReader packet)
		{
			var subCode = packet.ReadByte();

			switch (subCode)
			{
				case 4:
					Login(socket, packet);
					break;
			}
		}

		private void Login(Socket socket, PacketReader packet)
		{
			var credentials = PacketMapper.Map<LoginCredentials>(packet);

			if (credentials.Username.Length < 4 ||
				credentials.Username.Length > 14 ||
				credentials.Password.Length < 4 ||
				credentials.Password.Length > 14)
			{
				SendInvalidUsernameOrPassword(socket);
				return;
			}

			if (credentials.ClientVersion < 1102)
			{
				SendInvalidClientVersion(socket);
				return;
			}

			if (credentials.ItemDataFileLength.Length < 2 || credentials.ItemDataFileLength.Length > 15)
			{
				SendInvalidDataFile(socket);
				return;
			}

			try
			{
				var user = DataRepository.GetUser(credentials.Username, credentials.Password);
			}
			catch (UserNotFoundException)
			{
				SendInvalidUsernameOrPassword(socket);
			}
		}

		private static void SendInvalidDataFile(Socket socket)
		{
			var send = Packet.WriterFactoryAdapter();

			send.WriteBytes(new byte[] { 0, 65 });
			send.LastTransmission = true;

			socket.Enqueue(send);
		}

		private static void SendInvalidClientVersion(Socket socket)
		{
			var send = Packet.WriterFactoryAdapter();

			send.WriteBytes(new byte[] { 0, 17 });
			send.LastTransmission = true;

			socket.Enqueue(send);
		}

		private static void SendInvalidUsernameOrPassword(Socket socket)
		{
			var send = Packet.WriterFactoryAdapter();

			send.WriteBytes(new byte[] { 63, 2 });
			socket.Enqueue(send);

			send = Packet.WriterFactoryAdapter();

			send.WriteBytes(new byte[] { 1, 6 });
			send.LastTransmission = true;

			socket.Enqueue(send);
		}
	}
}