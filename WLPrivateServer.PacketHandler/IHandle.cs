using WLPrivateServer.Packets;
using WLPrivateServer.Sockets;
using WLPrivateServer.Users;

namespace WLPrivateServer.PacketHandler
{
	public interface IHandle
	{
		void Handle(User user, PacketReader packet);

		void Handle(Socket socket, PacketReader packet);
	}
}