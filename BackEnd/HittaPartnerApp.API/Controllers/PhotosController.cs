using AutoMapper;
using CloudinaryDotNet;
using HittaPartnerApp.API.Data;
using HittaPartnerApp.API.Helpers;
using HittaPartnerApp.API.Services.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IHittaPartnerRepo _hittaPartnerRepo;
        private readonly IOptions<CloudinarySettings> _cloudinarySettings;
        private readonly IMapper _mapper;

        public PhotosController(IHittaPartnerRepo  hittaPartnerRepo,IOptions< CloudinarySettings> cloudinarySettings,IMapper mapper)
        {
            _hittaPartnerRepo = hittaPartnerRepo;
           _cloudinarySettings = cloudinarySettings;
            _mapper = mapper;
            Account account = new Account(
                _cloudinarySettings.Value.CloudName,
                _cloudinarySettings.Value.ApiKey,
                _cloudinarySettings.Value.ApiSecret
                );

            Cloudinary cloudinary = new Cloudinary(account);
        }

        
    }
}
