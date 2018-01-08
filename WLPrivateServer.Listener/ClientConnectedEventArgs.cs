using System;
using System.Net.Sockets;

namespace WLPrivateServer.Listener
{
	public class ClientConnectedEventArgs : EventArgs
	{
		public Socket Client { get; private set; }

		public ClientConnectedEventArgs(Socket client)
		{
			Client = client;
		}
	}
}