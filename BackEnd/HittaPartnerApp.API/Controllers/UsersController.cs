using AutoMapper;
using HittaPartnerApp.API.Helpers;
using HittaPartnerApp.API.Services.DtoModels;
using HittaPartnerApp.API.Services.IRepositories;
using HittaPartnerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Controllers
{
    [ServiceFilter(typeof(UserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    public class UsersController : ControllerBase
    {
        private readonly IHittaPartnerRepo _hittaPartnerRepo;
        private readonly IMapper _mapper;

        public UsersController(IHittaPartnerRepo hittaPartnerRepo,IMapper mapper)
        {
            _hittaPartnerRepo = hittaPartnerRepo;
           _mapper = mapper;
        }
        /// <summary>
        /// Funktion som hämtar alla användaren 
        /// </summary>
        /// <returns>List av UserForListDto</returns>
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(200,Type =typeof(List<UserForListDto>))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAllUsers([FromQuery] UserParams userParams)
        {
            
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _hittaPartnerRepo.GetUserByID(currentUserId);
            userParams.UserId = currentUserId;
            //if (string.IsNullOrEmpty(userParams.Gender))
            //{
                userParams.Gender = currentUser.Gender=="Man" ? "Kvinna" : "Man";
            //}
            var users = await _hittaPartnerRepo.GetAllUsers(userParams);
            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            Response.AddPagination(users.CurrentPage, users.PagesSize, users.TotalCount, users.TotalPages);
            return Ok(userToReturn);
        }
        /// <summary>
        /// Funktion som hämtar en användare genom Iden
        /// </summary>
        /// <param name="userId">Iden av användaren</param>
        /// <returns>en UserForDetailsDto model</returns>
        [HttpGet("GetUserByID",Name ="GetUser")]
        [ProducesResponseType(200, Type = typeof(UserForDetailsDto))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult>GetUserByID(string userId)
        {
            var user = await _hittaPartnerRepo.GetUserByID(userId);
            var userToreturn = _mapper.Map<UserForDetailsDto>(user);
            return Ok(userToreturn);
        }
        /// <summary>
        /// Funktion för att uppdatera användarens info
        /// </summary>
        /// <param name="userID">ID:string</param>
        /// <param name="userForUpdateDto">userForUpdateDto model</param>
        /// <returns>204 </returns>
       
        [ProducesResponseType(204)]
        [ProducesDefaultResponseType]
        [HttpPut("UpdateUser")]
        public async Task<ActionResult>UpdateUser(string userID,UserForUpdateDto userForUpdateDto)
        {
            if(userID!=User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
            var userFromRepo = await _hittaPartnerRepo.GetUserByID(userID);
            _mapper.Map(userForUpdateDto, userFromRepo);
            if(await _hittaPartnerRepo.SaveAll())
            return NoContent();
            throw new Exception("Det uppstod ett problem vid ändring av abonnentens data");
        }
       
        /// <summary>
        /// Funktion för att skicka gilla till en annan medlem
        /// </summary>
        /// <param name="id">Änvandare Id som skickar gilla</param>
        /// <param name="recipientId">ID för användaren till vilken gillande skickades</param>
        /// <returns> Ok</returns>
        [HttpPost("SendLike")]
        [ProducesResponseType(200)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult>SendLike(string id,string recipientId)
        {
            if (id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }
            //kolla om den här användaren har gillat den medlemen innan?
            var like = await _hittaPartnerRepo.GetLike(id, recipientId);
            if (like != null) return BadRequest("Du gillade den här medlemmen tidigare");
            like = new Like()
            {
                LikerID = id,
                LikeeID=recipientId
            
            };
            _hittaPartnerRepo.Add(like);
            if (!await _hittaPartnerRepo.SaveAll()) return BadRequest("Det gick inte att gilla");
            return Ok();
        }

    }
}
