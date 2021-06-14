using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Services.DtoModels.MessageModel
{
    public class MessageForCreationDto
    {
     
        public string SenderID { get; set; }
        public string RecipientID { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime MessageSent { get; set; }
        public MessageForCreationDto()
        {
            this.MessageSent = DateTime.Now;
        }
      
    }
}
