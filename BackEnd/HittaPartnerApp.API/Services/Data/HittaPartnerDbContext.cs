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
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>()
                .HasKey(k => new { k.LikerID, k.LikeeID });
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Likee)
                .WithMany(u => u.GroupOfFansOfMe)
                .HasForeignKey(l => l.LikeeID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Like>()
                .HasKey(k => new { k.LikerID, k.LikeeID });
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Liker)
                .WithMany(u => u.GroupOfPeopleILike)
                .HasForeignKey(l => l.LikerID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                   .HasOne(m => m.Recipien)
                   .WithMany(u => u.MessagesReceived)
                   .OnDelete(DeleteBehavior.Restrict);



            base.OnModelCreating(modelBuilder);
        }

    }
}
