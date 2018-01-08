using System;
using System.Collections.Generic;
using System.Linq;

namespace WLPrivateServer.Packets
{
	public class PacketMapper
	{
		private static List<IPacketMappingProfile> profiles = new List<IPacketMappingProfile>();

		static PacketMapper()
		{
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(x => x.GetTypes())
				.Where(x => x != typeof(object) && !x.IsAbstract && typeof(PacketMapperConfig).IsAssignableFrom(x))
				.Select(x => Activator.CreateInstance(x) as PacketMapperConfig)
				.ToList();

			types.ForEach(x => x.Load());
		}

		public static PacketMappingProfile<TSource, TDestination> CreateMapping<TSource, TDestination>()
		{
			var profile = new PacketMappingProfile<TSource, TDestination>();

			profiles.Add(profile);

			return profile;
		}

		public static TDestination Map<TDestination>(object source)
		{
			var destType = typeof(TDestination);
			var sourceType = source.GetType();

			foreach (var profile in profiles)
			{
				var genericArgs = profile.GetType().GetGenericArguments();

				if (genericArgs[0] == sourceType && genericArgs[1] == destType)
					return (TDestination)profile.Map(source, null);
			}

			return default(TDestination);
		}
	}
}