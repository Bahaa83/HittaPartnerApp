using AutoMapper;
using HittaPartnerApp.API.Services.DtoModels;
using HittaPartnerApp.API.Services.DtoModels.MessageModel;
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
            CreateMap<User, UserForListDto>().ForMember(x => x.PhotoUrl, opt => opt.MapFrom(x => x.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(x => x.Age, opt => opt.ResolveUsing(x => x.DateOfBirth.CalculateAge()));
            CreateMap<User, UserForDetailsDto>().ForMember(x => x.PhotoUrl, opt => opt.MapFrom(x => x.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(x => x.Age, opt => opt.ResolveUsing(x => x.DateOfBirth.CalculateAge()));
               
                
            CreateMap<Photo, PhotoForDetailsDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>().ReverseMap();
            CreateMap<PhotoForUserDto,Photo>();
            CreateMap<UserForRegisterDto,User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>();


        }
    }
}
