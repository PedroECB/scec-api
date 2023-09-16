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

        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
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
