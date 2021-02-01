using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SWO.Models.DataModels;
using SWO.Shared.Models.ViewModels;

namespace SWO.Shared.MappingProfiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<Member, MemberViewModel>().ReverseMap();
        }
    }
}
