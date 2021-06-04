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
    public class HittaPartnerRepo : IHittaPartnerRepo
    {
        private readonly HittaPartnerDbContext _dbcontext;

        public HittaPartnerRepo(HittaPartnerDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Add<T>(T entity) where T : class
        {
            _dbcontext.Add(entity);
            
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbcontext.Remove(entity);
            
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
           var users= await _dbcontext.users.Include(x=>x.Photos).ToListAsync();
            return users;
        }

        public async Task<Photo> GetPhoto(string PhotoId)
        {
            var photo =await _dbcontext.Photos.FirstOrDefaultAsync(p => p.ID.Equals(PhotoId));
            return photo;
        }

        public async Task<User> GetUserByID(string UserID)
        {
            var user = await _dbcontext.users.Include(x=>x.Photos).FirstOrDefaultAsync(x => x.ID.Equals(UserID));
            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _dbcontext.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
