using System;
using System.Net;
using System.Net.Sockets;
using WLPrivateServer.TaskPool;

namespace WLPrivateServer.Listener
{
	public class SocketListener : IDisposable
	{
		public event EventHandler<ClientConnectedEventArgs> ClientConnected;

		private Socket serverSocket;

		private SocketListener(int port, int backlog)
		{
			serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));

			serverSocket.Listen(backlog);

			serverSocket.Blocking = false;
		}

		private void Listener()
		{
			try
			{
				var client = serverSocket.Accept();

				ClientConnected?.Invoke(this, new ClientConnectedEventArgs(client));
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode != SocketError.WouldBlock)
					throw;
			}
		}

		public void Listen()
		{
			TaskPoolManager.Enqueue(new RecurringTask(Listener, TimeSpan.FromMilliseconds(1)));
		}

		public void Close()
		{
			serverSocket.Close();
		}

		public static SocketListener Create(int port, int backlog)
		{
			return new SocketListener(port, backlog);
		}

		public void Dispose()
		{
			serverSocket.Dispose();
		}
	}
}