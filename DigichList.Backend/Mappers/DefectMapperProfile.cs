using AutoMapper;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;

namespace DigichList.Backend.Mappers
{
    public class DefectMapperProfile : Profile
    {
        public DefectMapperProfile()
        {
            CreateMap<Defect, DefectViewModel>()
                .ForMember(x => x.CreatedAt, 
                    y => y.MapFrom(src => src.CreatedAt.ToShortDateString()))
                
                .ForMember(x => x.Publisher, 
                    y => y.MapFrom(src => src.Publisher.ToString()))

                .ForMember(x => x.UserThatFixesDefect, 
                    y => y.MapFrom(src => src.AssignedDefect.AssignedWorker.ToString()))

                .ForMember(x => x.StatusChangedAt,
                    y => y.MapFrom(src => src.AssignedDefect.StatusChangedAt.Value.ToShortDateString()))

                .ForMember(x => x.DefectStatus, y => 
                {
                    y.MapFrom(src => src.AssignedDefect.Status.ToString());
                    y.NullSubstitute("Not Assigned");
                }).ReverseMap();
        }
    }
}
