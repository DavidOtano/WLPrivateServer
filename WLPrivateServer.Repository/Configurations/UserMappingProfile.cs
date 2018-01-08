using AutoMapper;

namespace WLPrivateServer.Repository.Configurations
{
	public class UserMappingProfile : Profile
	{
		public UserMappingProfile()
		{
			CreateMap<DataAccessLayer.Models.User, Users.User>()
				.ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
				.ForMember(x => x.Username, x => x.MapFrom(y => y.Username))
				.ForMember(x => x.Password, x => x.MapFrom(y => y.Password))
				.ForMember(x => x.Characters, x => x.MapFrom(y => y.Characters))
				.ReverseMap().ForAllOtherMembers(x => x.Ignore());
		}
	}
}