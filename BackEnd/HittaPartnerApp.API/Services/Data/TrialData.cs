using HittaPartnerApp.API.Data;
using HittaPartnerApp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Services.Data
{
    public class TrialData
    {
        private readonly DbContext _dbContext;

        public TrialData(HittaPartnerDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public void TrialUsers()
        {
            //Läsa alla filen och spara de i userData sen stäng filen
            var userData = System.IO.File.ReadAllText("Services/Data/UserTrialData.json");
             //konventrerar filen från json till list av User object.
             var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatPasswordHash("password", out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.UserName = user.UserName.ToLower();
                _dbContext.Add(user);

            }
            _dbContext.SaveChanges();
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
