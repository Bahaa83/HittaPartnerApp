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
        /// <summary>
        /// Funktion för att Lägga till ett foto och visa det
        /// </summary>
        /// <param name="userId">Användaren ID</param>
        /// <param name="photoForUserDto">PhotoForUserDto model</param>
        /// <returns>CreatedAtRoute till GetPhoto Funktion</returns>
        [HttpPost("AddPhotoForUser")]
        
        [ProducesDefaultResponseType]
        public async Task<ActionResult> AddPhotoForUser(string userId,[FromForm]PhotoForUserDto photoForUserDto)
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
#pragma warning disable CS0618 // Type or member is obsolete
            photoForUserDto.Url = uploadResult.Uri.ToString();
#pragma warning restore CS0618 // Type or member is obsolete
            photoForUserDto.PublicId = uploadResult.PublicId;
            var photo = _mapper.Map<Photo>(photoForUserDto);
            if (!userFromRepo.Photos.Any(p => p.IsMain))
                photo.IsMain = true;
            userFromRepo.Photos.Add(photo);
            if (await _hittaPartnerRepo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new {id=photo.ID}, photoToReturn);
            }
          
            return BadRequest("Fel vid tillägg av bild");

        }
        /// <summary>
        /// Funktion för att retunerar ett photo
        /// </summary>
        /// <param name="photoId">photo ID</param>
        /// <returns>Photo av PhotoForReturnDto</returns>
        [HttpGet(Name = "GetPhoto")]
        [ProducesResponseType(200,Type =typeof(PhotoForReturnDto))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult>GetPhoto(string photoId)
        {
            var photoFromRepo = await _hittaPartnerRepo.GetPhoto(photoId);
            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);
            return Ok(photo);
        }
        /// <summary>
        /// Funktion för att tillägg en bild som huvudbild
        /// </summary>
        /// <param name="userId">användare ID</param>
        /// <param name="photoId">Foto ID</param>
        /// <returns>204</returns>
        [HttpPost("SetMainPhotoForUser")]
        [ProducesResponseType(204)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SetMainPhotoForUser(string userId,string photoId)
        {
            if(userId!=User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            var desiredPhoto = await _hittaPartnerRepo.GetPhoto(photoId);
            if (desiredPhoto.IsMain) return BadRequest("Det här är redan huvudbild");
            var oldMainPhoto = await _hittaPartnerRepo.GetMainPhotoForUser(userId);
            oldMainPhoto.IsMain = false;
            desiredPhoto.IsMain = true;
            if (!await _hittaPartnerRepo.SaveAll()) return BadRequest("Fel vid uppdatering av huvudbilden");
            return NoContent();
        }

    }
}
