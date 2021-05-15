using HittaPartnerApp.API.Data;
using HittaPartnerApp.API.Services.IRepositories;
using HittaPartnerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Services.Repositories
{
    public class Authentication : IAuthentication
    {
        private readonly HittaPartnerDbContext db;

        public Authentication(HittaPartnerDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> IsExists(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Login(string UserName, string Password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(User user, string Password)
        {
            byte[] passwordHash, passwordSalt;
            CreatPasswordHash( Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await db.users.AddAsync(user);
            await db.SaveChangesAsync(); 
            return user;
        }

        private void CreatPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
