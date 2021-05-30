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
        public string DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction  { get; set; }
        public string LookingFor { get; set; }
        public string Inerests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
