using System;
using System.Collections.Generic;
using System.Text;

namespace HittaPartnerApp.Models
{
   public class User:Record
    {
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
