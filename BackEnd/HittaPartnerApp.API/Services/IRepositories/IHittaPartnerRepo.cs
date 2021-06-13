using HittaPartnerApp.API.Helpers;
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
        Task<PagedList<User>> GetAllUsers(UserParams userParams);
        Task<User> GetUserByID(string UserID);
        Task<bool> SaveAll();
        Task<Photo> GetPhoto(string PhotoId);
        Task<Photo> GetMainPhotoForUser(string userId);
        Task<Like> GetLike(string Id, string recipientId);
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetConfersation(int userId,int recipientId);
    }
}
