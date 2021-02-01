using AutoMapper;
using SWO.Models.DataModels;
using SWO.Shared.Models.ViewModels;

namespace SWO.Shared.MappingProfiles
{
    public class SimulatorProfile : Profile
    {
        public SimulatorProfile()
        {
            CreateMap<Simulator, SimulatorViewModel>().ReverseMap();
        }
    }
}
