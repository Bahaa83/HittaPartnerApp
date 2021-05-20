using HittaPartnerApp.API.Services.DtoModels;
using HittaPartnerApp.API.Services.IRepositories;
using HittaPartnerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private readonly IConfiguration _config;

        public AccountsController(IAuthentication repo,IConfiguration config)
        {
            _repo = repo;
            _config = config;
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
        /// <summary>
        /// Loggin 
        /// </summary>
        /// <param name="userForLoginDto">UserForLoginDto model från klientsida </param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult>Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);
            if (userFromRepo == null) return Unauthorized();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.ID),
                new Claim(ClaimTypes.Name,userFromRepo.UserName)
            };
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
               
            });
        }
    }
}
