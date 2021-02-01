using AutoMapper;
using SWO.Models.DataModels;
using SWO.Shared.Models.ViewModels;

namespace SWO.Shared.MappingProfiles
{
    public class GradeTemplateProfile : Profile
    {
        public GradeTemplateProfile()
        {
            CreateMap<GradeTemplate, GradeTemplateViewModel>().ReverseMap();
        }
    }
}
