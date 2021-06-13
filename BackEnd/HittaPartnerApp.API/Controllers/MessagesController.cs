using AutoMapper;
using HittaPartnerApp.API.Helpers;
using HittaPartnerApp.API.Services.DtoModels.MessageModel;
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
    public class MessagesController : ControllerBase
    {
        private readonly IHittaPartnerRepo _hittaPartnerRepo;
        private readonly IMapper _mapper;

        public MessagesController(IHittaPartnerRepo hittaPartnerRepo,IMapper mapper)
        {
            _hittaPartnerRepo = hittaPartnerRepo;
            _mapper = mapper;
        }
        /// <summary>
        /// Funktion för att hämta en meddelande
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpGet("GetMessage",Name ="GetMessage")]
        [ProducesResponseType(200)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult>GetMessage(string userId,int messageId)
        {
            if(userId!= User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            var messageFromRepo = await _hittaPartnerRepo.GetMessage(messageId);
            if(messageFromRepo==null)
            {
                return NotFound();
            }
            return Ok(messageFromRepo);
        }
        /// <summary>
        /// Funktion för att skåpa meddelande
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="messageForCreation">messageForCreation model</param>
        /// <returns> MessageForCreationDto</returns>
        [HttpPost("CreateMessage")]
        [ProducesResponseType(201)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateMessage(string userId, MessageForCreationDto messageForCreation )
        {

            if (userId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            messageForCreation.SenderID = userId;
            var recipient = await _hittaPartnerRepo.GetUserByID(messageForCreation.RecipienID);
            if (recipient == null) return BadRequest("Mottagaren är inte tillgänglig");
            var message = _mapper.Map<Message>(messageForCreation);
            _hittaPartnerRepo.Add(message);
            var messageToReturn = _mapper.Map<MessageForCreationDto>(message);
            if (await _hittaPartnerRepo.SaveAll())
            return CreatedAtRoute("GetMessage", new {id=message.ID }, messageToReturn);
            throw new Exception();
        }
        /// <summary>
        /// Funktion returnerar Alla meddelanden
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="messageParams"></param>
        /// <returns> List av MessageToReturnDto </returns>
        [HttpGet("GetMessagesForUser")]
        [ProducesResponseType(200)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult>GetMessagesForUser(string userId,[FromQuery]MessageParams messageParams)
        {
            if (userId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            messageParams.UserId = userId;
           var messagesFromRepo = await _hittaPartnerRepo.GetMessagesForUser(messageParams);
            var messages = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);
            Response.AddPagination(messagesFromRepo.CurrentPage,messagesFromRepo.PagesSize,messagesFromRepo.TotalCount,messagesFromRepo.TotalPages);
            return Ok(messages);
        }
        /// <summary>
        /// Funktion För att hämta alla meddelander mellan avsändare och mottagaren
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="recipientId"></param>
        /// <returns> lista av MessageToReturnDto</returns>
        [HttpGet("GetConversation")]
        [ProducesResponseType(200)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetConversation(string userId,string recipientId)
        {
            if (userId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            var messagesFromRepo = await _hittaPartnerRepo.GetConversation(userId, recipientId);
            var messageToreturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);
            return Ok(messageToreturn);
        }
    }
}
