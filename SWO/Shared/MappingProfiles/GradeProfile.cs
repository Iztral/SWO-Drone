﻿using AutoMapper;
using SWO.Models.DataModels;
using SWO.Shared.Models.ViewModels;

namespace SWO.Shared.MappingProfiles
{
    public class GradeProfile : Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, GradeViewModel>().ReverseMap();
        }
    }
}
