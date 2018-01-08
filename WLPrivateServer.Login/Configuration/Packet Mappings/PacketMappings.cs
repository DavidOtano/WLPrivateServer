using System.Linq;
using System.Text;
using WLPrivateServer.Login.Implementations;
using WLPrivateServer.Packets;

namespace WLPrivateServer.Login.Configuration.Packet_Mappings
{
	public class PacketMappings : PacketMapperConfig
	{
		public override void Load()
		{
			CreateMapping<PacketReader, LoginCredentials>()
				.ForMember(x => x.Username, x => x.MapFrom(y => y.ReadString(y.ReadByte())))
				.ForMember(x => x.Password, x => x.MapFrom(y => y.ReadString(y.ReadByte())))
				.ForMember(x => x.ClientVersion, x => x.MapFrom(y => y.ReadUShort()))
				.ForMember(x => x.ItemDataFileLength, x => x.MapFrom(y =>
				{
					byte len = y.ReadByte();
					byte xor = y.ReadByte();
					byte[] bytes = y.ReadBytes(len).Select(b => (byte)(b ^ xor)).ToArray();

					return Encoding.ASCII.GetString(bytes);
				}));
		}
	}
}