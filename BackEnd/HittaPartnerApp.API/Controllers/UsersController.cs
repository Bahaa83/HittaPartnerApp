using AutoMapper;
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
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _hittaPartnerRepo.GetAllUsers();
            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
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
        [Authorize]
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

    }
}
