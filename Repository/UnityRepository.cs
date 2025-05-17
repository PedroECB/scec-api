using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCEC.API.Models;
using SCEC.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace SCEC.API.Repository
{
    public class GroupUnityRepository : IRepository<GroupUnity>
    {
        private DataContext _context;

        public GroupUnityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupUnity>> GetAll()
        {
            IEnumerable<GroupUnity> groupUnityList = new List<GroupUnity>();

            groupUnityList = await _context.GroupUnity
                .Where(g=> g.Enabled.Equals('Y'))
                .AsNoTracking()
                .ToListAsync();

            return groupUnityList;
        }

        public Task<GroupUnity> Add(GroupUnity entity)
        {
            throw new NotImplementedException();
        }

        public Task<GroupUnity> Update(GroupUnity entity)
        {
            throw new NotImplementedException();
        }

        public Task<GroupUnity> Delete(GroupUnity entity)
        {
            throw new NotImplementedException();
        }

        public Task<GroupUnity> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
