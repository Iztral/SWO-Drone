using AutoMapper;
using SWO.Models.DataModels;
using SWO.Shared.Models.ViewModels;

namespace SWO.Shared.MappingProfiles
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            CreateMap<Instructor, InstructorViewModel>().ReverseMap();
        }
    }
}
