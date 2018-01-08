using System;
using System.Linq;
using System.Text;
using WLPrivateServer.Bootstrapper;
using WLPrivateServer.Items.Data;
using WLPrivateServer.Listener;
using WLPrivateServer.Login;
using WLPrivateServer.Packets;
using WLPrivateServer.TaskPool;

namespace WLPrivateServer
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			InitializePacketLibrary();

			LoadItems();

			WLPrivateServerBootstrapper.Initialize();
			LoginManager.Initialize();

			using (var listener = SocketListener.Create(6414, 50))
			{
				listener.Listen();

				listener.ClientConnected += Listener_ClientConnected;

				Console.Read();

				TaskPoolManager.Stop();
				listener.Close();
			}
		}

		private static void LoadItems()
		{
			Console.WriteLine("Loading items...");
			ItemDataFile.LoadItems("C:\\Program Files (x86)\\Wonderland Online\\data\\item.dat");
			Console.WriteLine("Items Loaded!");
		}

		private static void InitializePacketLibrary()
		{
			Packet.DefaultEncoding = Encoding.ASCII;
			Packet.PacketEncoding = x => x.Select(b => (byte)(b ^ 0xAD)).ToArray();
			Packet.PacketDecoding = x => x.Select(b => (byte)(b ^ 0xAD)).ToArray();
			Packet.BuildPacketHeader = x => BitConverter.GetBytes((ushort)0x44F4).Concat(BitConverter.GetBytes((ushort)x.Length)).Concat(x).ToArray();
		}

		private static void Listener_ClientConnected(object sender, ClientConnectedEventArgs e)
		{
			LoginManager.Add(e.Client);
		}
	}
}