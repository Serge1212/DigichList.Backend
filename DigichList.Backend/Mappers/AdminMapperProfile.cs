using AutoMapper;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;

namespace DigichList.Backend.Mappers
{
    public class AdminMapperProfile : Profile
    {
        public AdminMapperProfile()
        {
            CreateMap<Admin, AdminViewModel>()
                .ForMember(x => x.AccessLevel, y => y.MapFrom(src => src.AccessLevel.ToString()));
        }
    }
}
