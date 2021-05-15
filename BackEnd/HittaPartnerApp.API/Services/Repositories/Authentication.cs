using HittaPartnerApp.API.Data;
using HittaPartnerApp.API.Services.IRepositories;
using HittaPartnerApp.Models;
using Microsoft.EntityFrameworkCore;
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
            
            if (await db.users.AnyAsync(x => x.UserName.Equals(UserName))) return true;
            return false;
        }

        public async Task<User> Login(string UserName, string Password)
        {
            var user = await db.users.FirstOrDefaultAsync(x => x.UserName.Equals(UserName));
            if (user == null) return null;
            if ( !VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt)) return null;
            return user;

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               
               var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if(ComputedHash[i]!= passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
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
