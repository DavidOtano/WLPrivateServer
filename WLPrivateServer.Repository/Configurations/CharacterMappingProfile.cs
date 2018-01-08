using AutoMapper;

namespace WLPrivateServer.Repository.Configurations
{
	public class CharacterMappingProfile : Profile
	{
		public CharacterMappingProfile()
		{
			CreateMap<DataAccessLayer.Models.Character, Characters.Character>()
				.ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
				.ForMember(x => x.Name, x => x.MapFrom(y => y.Name))
				.ForMember(x => x.NickName, x => x.MapFrom(y => y.NickName))
				.ForMember(x => x.Level, x => x.MapFrom(y => y.Level))
				.ForMember(x => x.Rebirth, x => x.MapFrom(y => y.Rebirth))
				.ForMember(x => x.STR, x => x.MapFrom(y => y.STR))
				.ForMember(x => x.CON, x => x.MapFrom(y => y.CON))
				.ForMember(x => x.INT, x => x.MapFrom(y => y.INT))
				.ForMember(x => x.WIS, x => x.MapFrom(y => y.WIS))
				.ForMember(x => x.AGI, x => x.MapFrom(y => y.AGI))
				.ForMember(x => x.CurrentHP, x => x.MapFrom(y => y.CurrentHP))
				.ForMember(x => x.CurrentSP, x => x.MapFrom(y => y.CurrentSP))
				.ReverseMap().ForAllOtherMembers(x => x.Ignore());
		}
	}
}