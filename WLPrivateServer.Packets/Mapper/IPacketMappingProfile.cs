using System;

namespace WLPrivateServer.Packets
{
	public interface IPacketMappingProfile
	{
		object Map(object source, Func<object> destinationFactory);
	}
}