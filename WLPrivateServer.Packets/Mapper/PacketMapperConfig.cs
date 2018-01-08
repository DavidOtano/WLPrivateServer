namespace WLPrivateServer.Packets
{
	public abstract class PacketMapperConfig
	{
		public PacketMapperConfig()
		{
		}

		public abstract void Load();

		public PacketMappingProfile<TSource, TDestination> CreateMapping<TSource, TDestination>()
		{
			return PacketMapper.CreateMapping<TSource, TDestination>();
		}
	}
}