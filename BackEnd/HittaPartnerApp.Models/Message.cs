﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HittaPartnerApp.Models
{
   public class Message
    {
        public int ID { get; set; }
        public string SenderID { get; set; }
        public User Sender { get; set; }
        public string RecipientID { get; set; }
        public User Recipien { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; } 
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
        public bool  SenderDeleted { get; set; }
        public bool RecipienDeleted { get; set; }

    }
}
