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
    public class RoleRepository : IRepository<Role>
    {
        private DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            IEnumerable<Role> roles;
            roles = await _context.Roles.AsNoTracking().ToListAsync();
            return roles;
        }

        async Task<Role> IRepository<Role>.GetById(int id)
        {
            Role role = await _context.Roles.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return role;
        }

        public Task<Role> Add(Role entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Role> Update(Role entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Role> Delete(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
