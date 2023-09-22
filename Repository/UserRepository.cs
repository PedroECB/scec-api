using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCEC.API.Models;
using SCEC.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(int id)
        {
            User user = await _context.Users.Where(x => x.Id == id)
                        .AsNoTracking()
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


        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
