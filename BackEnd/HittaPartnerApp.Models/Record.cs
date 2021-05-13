using System;
using System.Collections.Generic;
using System.Text;

namespace HittaPartnerApp.Models
{
   public class Record
    {
        public string ID { get; set; }
        public Record()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}
