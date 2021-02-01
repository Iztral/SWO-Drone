using AutoMapper;
using SWO.Models.DataModels;
using SWO.Shared.Models.ViewModels;

namespace SWO.Shared.MappingProfiles
{
    public class SimulationProfile : Profile
    {
        public SimulationProfile()
        {
            CreateMap<Simulation, SimulatorViewModel>().ReverseMap();
        }
    }
}
