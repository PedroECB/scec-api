using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCEC.API.Models;
using SCEC.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static SCEC.API.Settings;
using SimpleCrypto;

namespace SCEC.API.Repository
{
    public class UserRepository : IRepository<User>
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        #region BASIC METHODS

        public async Task<User> Add(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(User user)
        {
            user.Enabled = CONSTANTS.FLAG_NO;
            user.LastUpdate = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetById(int id)
        {
            User user = await _context.Users.Where(x => x.Id == id)
                            .Select(x => new User() { Id = x.Id, Name = x.Name, Uuid = x.Uuid, Email = x.Email, Enabled = x.Enabled, CreatedAt = x.CreatedAt })
                            .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            IEnumerable<User> users = new List<User>();
            users = await _context.Users.AsNoTracking().ToListAsync();
            return users;
        }


        public async Task<User> Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        #endregion

        #region OTHERS METHODS

        /// <summary>
        /// Method to find user by email (AsNoTracking)
        /// </summary>
        /// <param name="userEmail">User e-mail adress.</param>
        /// <returns>
        /// (User) user found or null when not found
        /// </returns>
        public async Task<User> GetByEmail(string userEmail)
        {
            User user = await _context.Users.Where(x => x.Email.Equals(userEmail) && x.Enabled.Equals(CONSTANTS.FLAG_YES))
                                        .AsNoTracking()
                                        .Select(x => new User { Id = x.Id, Name = x.Name, Email = x.Email, Password = x.Password, Salt = x.Salt })
                                        .FirstOrDefaultAsync();
            return user;
        }

        /// <summary>
        /// Alternative method to find user by id, returning full columns of entity
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>
        /// (User) user found or null when not found
        /// </returns>
        public async Task<User> GetUserByIdFullColumns(int id)
        {
            User user = await _context.Users.Where(x => x.Id == id)
                 .Select(x => new User() { Id = x.Id, Name = x.Name, Uuid = x.Uuid, Email = x.Email, Enabled = x.Enabled, CreatedAt = x.CreatedAt, Password = x.Password, Salt = x.Salt, LastUpdate = x.LastUpdate })
                 .FirstOrDefaultAsync();
            return user;
        }

        #endregion

        #region USER UTILS

        /// <summary>
        /// Method to set user user password
        /// </summary>
        /// <param name="user">User object will have the password changed</param>
        /// <returns>
        /// void
        /// </returns>
        public void SetPassword(ref User user, string newPassword)
        {
            PBKDF2 crypto = new PBKDF2();
            user.Password = crypto.Compute(newPassword);
            user.Salt = crypto.Salt;
            user.LastUpdate = DateTime.Now;
        }

        #endregion
    }
}
