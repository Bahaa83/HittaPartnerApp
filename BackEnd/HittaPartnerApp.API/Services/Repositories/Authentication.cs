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
            throw new NotImplementedException();
        }
    }
}
