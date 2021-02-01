using AutoMapper;
using SWO.Models.DataModels;
using SWO.Shared.Models.ViewModels;

namespace SWO.Shared.MappingProfiles
{
    public class ScenarioProfile : Profile
    {
        public ScenarioProfile()
        {
            CreateMap<Scenario, ScenarioViewModel>().ReverseMap();
        }
    }
}
