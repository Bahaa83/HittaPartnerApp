using HittaPartnerApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Data
{
    public class HittaPartnerDbContext:DbContext
    {
        public HittaPartnerDbContext(DbContextOptions<HittaPartnerDbContext> options) : base(options) { }
        
        public DbSet<User> users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        override

}
}
