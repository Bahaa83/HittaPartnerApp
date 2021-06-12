using System;
using System.Collections.Generic;
using System.Text;

namespace HittaPartnerApp.Models
{
   public class Like
    {
        public string LikerID { get; set; }
        public string LikeeID { get; set; }
        public User Liker { get; set; }
        public User Likee { get; set; }
    }
}
