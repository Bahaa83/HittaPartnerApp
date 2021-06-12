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
            var users = _dbcontext.users.Include(x => x.Photos).OrderByDescending(u=>u.LastActive).AsQueryable();
            users = users.Where(u =>u.ID!=userParams.UserId);
            users = users.Where(u => u.Gender.Equals(userParams.Gender));
            if(userParams.likers) 
            {
                //hämta id for de som gilla nuvarande användaren
                var userLikers = GetUserLikes(userParams.UserId, userParams.likers);
                //hämta medlemmar som de gillar mig ;)
                users = users.Where(u => userLikers.Result.Contains(u.ID));
            }
            if (userParams.likees)
            {
                var userLikees = GetUserLikes(userParams.UserId, userParams.likers);
                //hämta medlemmar som jag gillade de ;)
                users = users.Where(u => userLikees.Result.Contains(u.ID));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);
                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }

            if(!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                    users = users.OrderByDescending(u => u.created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Like> GetLike(string userId, string recipientId)
        {
            return await _dbcontext.Likes.FirstOrDefaultAsync(l => l.LikerID.Equals(userId)
            && l.LikeeID.Equals(recipientId));
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

        private async Task<List<string>> GetUserLikes(string id,bool likers)
        {
            var user = await _dbcontext.users.Include(u => u.GroupOfFansOfMe).Include(u => u.GroupOfPeopleILike)
                .FirstOrDefaultAsync(u => u.ID.Equals(id));
            if(likers)
            {

                return user.GroupOfFansOfMe.Where(u => u.LikeeID.Equals(id)).Select(l => l.LikerID).ToList();
            }
            else
            {
                return user.GroupOfPeopleILike.Where(u => u.LikerID.Equals(id)).Select(l => l.LikeeID).ToList();
            }
        }
    }
}
