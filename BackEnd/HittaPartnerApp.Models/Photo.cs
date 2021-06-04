using System;

namespace HittaPartnerApp.Models
{
    public class Photo:Record
    {
        public string Url { get; set; }
        public string PublicId { get; set; }
        public string Description { get; set; }
        public DateTime  DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string UserID { get; set; }
        public User user { get; set; }
    }
}