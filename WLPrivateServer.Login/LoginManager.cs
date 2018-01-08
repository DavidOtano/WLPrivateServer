using System;
using System.Collections.Generic;
using System.Linq;
using WLPrivateServer.PacketHandler;
using WLPrivateServer.Sockets;
using WLPrivateServer.TaskPool;

namespace WLPrivateServer.Login
{
	public class LoginManager
	{
		private static readonly object syncRoot = new object();
		private static List<Socket> sockets = new List<Socket>();

		public static void Initialize()
		{
			TaskPoolManager.Enqueue(new RecurringTask(Process, TimeSpan.FromMilliseconds(1)));
		}

		public static void Add(System.Net.Sockets.Socket socket)
		{
			lock (syncRoot)
			{
				sockets.Add(new Socket(socket));
			}
		}

		public static void Remove(Socket socket)
		{
			lock (syncRoot)
			{
				sockets.Remove(socket);
			}
		}

		public static void Process()
		{
			lock (syncRoot)
			{
				foreach (var socket in sockets.ToList())
				{
					try
					{
						socket.Read();
						socket.Write();

						var packet = socket.Dequeue();

						while (packet != null)
						{
							Handler.Handle(socket, packet);

							packet = socket.Dequeue();
						}
					}
					catch (SocketClosedException)
					{
						sockets.Remove(socket);
						socket.Close();
						socket.Dispose();
					}
				}
			}
		}
	}
}