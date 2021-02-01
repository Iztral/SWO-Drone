using AutoMapper;
using SWO.Models.DataModels;
using SWO.Shared.Models.ViewModels;

namespace SWO.Shared.MappingProfiles
{
    public class ScenarioGradesTemplatesProfile : Profile
    {
        public ScenarioGradesTemplatesProfile()
        {
            CreateMap<ScenarioGradesTemplates, ScenarioGradesTemplatesViewModel>().ReverseMap();
        }
    }
}
