using AutoMapper;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;

namespace DigichList.Backend.Mappers
{
    public class RoleMapperProfile : Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<Role, RoleViewModel>()
                .ForMember(x => x.RoleName, y => y.MapFrom(src => src.Name));

            CreateMap<Role, ExtendedRoleViewModel>()
                .ForMember(x => x.RoleName, y => y.MapFrom(src => src.Name));
        }
    }
}
