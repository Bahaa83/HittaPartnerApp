using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HittaPartnerApp.API.Data;
using HittaPartnerApp.API.Helpers;
using HittaPartnerApp.API.Services.DtoModels;
using HittaPartnerApp.API.Services.IRepositories;
using HittaPartnerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private Cloudinary cloudinary;

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

             cloudinary = new Cloudinary(account);
        }

        [HttpPost("AddPhotoForUser")]
        [Obsolete]
        public async Task<ActionResult> AddPhotoForUser(string userId,PhotoForUserDto photoForUserDto)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
           var  userFromRepo = await _hittaPartnerRepo.GetUserByID(userId);
            var file = photoForUserDto.File;
            var uploadResult = new ImageUploadResult();
            if(file!=null&& file.Length>0)
            {
                 using(var stream = file.OpenReadStream())
                {
                    var uploadparams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation()
                        .Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = cloudinary.Upload(uploadparams);
                }
            }
            photoForUserDto.Url = uploadResult.Uri.ToString();
            photoForUserDto.PublicId = uploadResult.PublicId;
            var photo = _mapper.Map<Photo>(photoForUserDto);
            if (!userFromRepo.Photos.Any(p => p.IsMain))
                photo.IsMain = true;
            userFromRepo.Photos.Add(photo);
            if (await _hittaPartnerRepo.SaveAll())
                return Ok();
            return BadRequest("Fel vid tillägg av bild");

        }
        

    }
}
