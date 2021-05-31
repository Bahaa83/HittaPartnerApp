using AutoMapper;
using HittaPartnerApp.API.Services.DtoModels;
using HittaPartnerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Helpers
{
    public class HittaPartnerProfile:Profile
    {
        public HittaPartnerProfile()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailsDto>();
            CreateMap<Photo, PhotoForDetailsDto>();
        }
    }
}
