using HittaPartnerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Services.IRepositories
{
   public interface IHittaPartnerRepo
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByID(string UserID);
        Task<bool> SaveAll();
        Task<Photo> GetPhoto(string PhotoId);
    }
}
