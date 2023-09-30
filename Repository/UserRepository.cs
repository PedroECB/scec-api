using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCEC.API.Models;
using SCEC.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static SCEC.API.Settings;

namespace SCEC.API.Repository
{
    public class UserRepository : IRepository<User>
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

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
                        .Select(x => new User()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Uuid = x.Uuid,
                            Email = x.Email,
                            Enabled = x.Enabled,
                            CreatedAt = x.CreatedAt
                        })
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
    }
}
