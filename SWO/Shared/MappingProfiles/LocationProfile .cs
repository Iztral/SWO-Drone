using AutoMapper;
using SWO.Models.DataModels;
using SWO.Shared.Models.ViewModels;

namespace SWO.Shared.MappingProfiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationViewModel>().ReverseMap();
        }
    }
}