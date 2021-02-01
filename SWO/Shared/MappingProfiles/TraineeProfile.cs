using AutoMapper;
using SWO.Models.DataModels;
using SWO.Shared.Models.ViewModels;

namespace SWO.Shared.MappingProfiles
{
    public class TraineeProfile : Profile
    {
        public TraineeProfile()
        {
            CreateMap<Trainee, TraineeViewModel>().ReverseMap();
        }
    }
}
