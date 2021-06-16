using AutoMapper;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;

namespace DigichList.Backend.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.RoleName, y => y.MapFrom(src => src.Role.Name))
                .ForMember(x => x.RoleId, y => y.MapFrom(src => src.Role.Id));

            CreateMap<User, TechnicianViewModel>();
               
        }
    }
}
