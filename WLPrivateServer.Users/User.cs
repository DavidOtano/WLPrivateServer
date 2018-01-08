using System.Collections.Generic;
using WLPrivateServer.Characters;
using WLPrivateServer.Packets;
using WLPrivateServer.Sockets;

namespace WLPrivateServer.Users
{
	public class User
	{
		private readonly object syncRoot = new object();
		private readonly Socket socket;

		private int id;
		private string username;
		private string password;
		private List<Character> characters = new List<Character>();

		public User()
		{
		}

		public int Id
		{
			get
			{
				lock (syncRoot)
				{
					return id;
				}
			}
			set
			{
				lock (syncRoot)
				{
					id = value;
				}
			}
		}

		public string Username
		{
			get
			{
				lock (syncRoot)
				{
					return username;
				}
			}
			set
			{
				lock (syncRoot)
				{
					username = value;
				}
			}
		}

		public string Password
		{
			get
			{
				lock (syncRoot)
				{
					return password;
				}
			}
			set
			{
				lock (syncRoot)
				{
					password = value;
				}
			}
		}

		public List<Character> Characters
		{
			get
			{
				lock (syncRoot)
				{
					return characters;
				}
			}
			set
			{
				lock (syncRoot)
				{
					characters = value;
				}
			}
		}

		private User(Socket socket)
		{
			this.socket = socket;
		}

		public void Read()
		{
			socket.Read();
		}

		public void Write()
		{
			socket.Write();
		}

		public void Enqueue(object obj)
		{
			socket.Enqueue(obj);
		}

		public Packet Dequeue()
		{
			return socket.Dequeue();
		}
	}
}