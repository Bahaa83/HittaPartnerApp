using HittaPartnerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Services.DtoModels.MessageModel
{
    public class MessageToReturnDto
    {
        public int ID { get; set; }
        public string SenderID { get; set; }
        public string SenderKnownAs { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string RecipientID { get; set; }
        public string RecipientKnownAs { get; set; }
        
        public string RecipientPhotoUrl { get; set; }

        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
        
    }
}
