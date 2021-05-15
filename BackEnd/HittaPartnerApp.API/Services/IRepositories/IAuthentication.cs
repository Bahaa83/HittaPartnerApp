using HittaPartnerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Services.IRepositories
{
    public interface IAuthentication
    {
        Task<User> Register(User user, string Password);
        Task<User> Login(string UserName, string Password);
        Task<bool> IsExists(string UserName);
    }
}
