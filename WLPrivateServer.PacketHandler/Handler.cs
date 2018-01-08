using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WLPrivateServer.Packets;
using WLPrivateServer.Sockets;
using WLPrivateServer.Users;

namespace WLPrivateServer.PacketHandler
{
	public static class Handler
	{
		private static Dictionary<HandlesAttribute, IHandle> handlers = new Dictionary<HandlesAttribute, IHandle>();

		static Handler()
		{
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(x => x.GetTypes())
				.Where(x => x.IsClass && x != typeof(object) &&
						typeof(IHandle).IsAssignableFrom(x))
						.ToList();

			foreach (var handler in types)
			{
				var handlesAttr = handler.GetCustomAttribute(typeof(HandlesAttribute)) as HandlesAttribute;

				if (handlesAttr != null)
					handlers.Add(handlesAttr, Activator.CreateInstance(handler) as IHandle);
			}
		}

		public static void Handle(Socket socket, PacketReader packet)
		{
			if (packet.Length > 4)
			{
				var code = GetCode(packet);

				var resultSet = GetHandlers(code);

				foreach (var result in resultSet)
					result.Value.Handle(socket, packet);
			}
		}

		private static List<KeyValuePair<HandlesAttribute, IHandle>> GetHandlers(byte code)
		{
			var resultSet = handlers.Where(x => x.Key.Code == code).ToList();

			if (!resultSet.Any())
				throw new PacketNotHandledException($"No handler exists for AC{code}");
			return resultSet;
		}

		private static byte GetCode(PacketReader packet)
		{
			packet.ReadPosition = 4;
			var code = packet.ReadByte();
			return code;
		}

		public static void Handle(User user, PacketReader packet)
		{
			if (packet.Length > 4)
			{
				var code = GetCode(packet);

				var resultSet = GetHandlers(code);

				foreach (var result in resultSet)
					result.Value.Handle(user, packet);
			}
		}
	}
}