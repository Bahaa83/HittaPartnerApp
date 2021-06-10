﻿using HittaPartnerApp.API.Data;
using HittaPartnerApp.API.Helpers;
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

        public async Task<PagedList<User>> GetAllUsers(UserParams userParams)
        {
           var users=  _dbcontext.users.Include(x=>x.Photos);

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Photo> GetMainPhotoForUser(string userId)
        {
            var mainPhoto = await _dbcontext.Photos.Where(p => p.UserID.Equals(userId)).FirstOrDefaultAsync(p=>p.IsMain);
            return mainPhoto;
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
