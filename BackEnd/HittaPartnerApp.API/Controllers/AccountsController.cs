using HittaPartnerApp.API.Services.DtoModels;
using HittaPartnerApp.API.Services.IRepositories;
using HittaPartnerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]


    public class AccountsController : ControllerBase
    {
        private readonly IAuthentication _repo;

        public AccountsController(IAuthentication repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Registrera ett nytt användare
        /// </summary>
        /// <param name="userForRegisterDto">UserForRegisterDto från klient sida</param>
        /// <returns>Status Code 201</returns>
        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult>Register(UserForRegisterDto userForRegisterDto)
        {

            if (await _repo.IsExists(userForRegisterDto.UserName.ToLower()))
                return BadRequest("Den här användaren är finns redan");
            var newUser = new User()
            {
                UserName= userForRegisterDto.UserName
            };
            var CreatedUser = await _repo.Register(newUser, userForRegisterDto.Password);
            if (CreatedUser == null) return BadRequest();
            return StatusCode(201);
        }
    }
}
