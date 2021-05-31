using HittaPartnerApp.API.Services.IRepositories;
using HittaPartnerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public UsersController(IHittaPartnerRepo hittaPartnerRepo)
        {
            _hittaPartnerRepo = hittaPartnerRepo;
        }
        /// <summary>
        /// Funktion som hämtar alla användaren 
        /// </summary>
        /// <returns>List av users</returns>
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(200,Type =typeof(List<User>))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _hittaPartnerRepo.GetAllUsers();
            return Ok(users);
        }
        /// <summary>
        /// Funktion som hämtar en användare genom Iden
        /// </summary>
        /// <param name="userId">Iden av användaren</param>
        /// <returns>en user model</returns>
        [HttpGet("GetUserByID")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult>GetUserByID(string userId)
        {
            var user = await _hittaPartnerRepo.GetUserByID(userId);
            return Ok(user);
        }
    }
}
