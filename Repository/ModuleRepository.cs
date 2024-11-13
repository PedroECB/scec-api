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
    public class ModuleRepository : IRepository<Module>
    {
        private DataContext _context;

        public ModuleRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Module> Add(Module entity)
        {
            throw new NotImplementedException();
        }

        public Task<Module> Delete(Module entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Module>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Module> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Module> Update(Module entity)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetModulesByRole(List<long> idsRoles)
        {
            List<ModuleRole> modulesRoles = await _context.ModulesRoles
                .Include("Module")
                .Where(x => idsRoles.Contains(x.Idrole))
                .AsNoTracking()
                .ToListAsync();

            IEnumerable<Module> parentModules = modulesRoles.Select(x => x.Module)
                .Where(x => x.ParentId == null && x.Enabled.Equals(CONSTANTS.FLAG_YES))
                .OrderBy(x => x.Order)
                .Select(x => new Module { IdModule = x.IdModule, Description = x.Description, AliasFlag = x.AliasFlag, Nested = x.Nested, UrlBase = x.UrlBase, Icon = x.Icon })
                .ToList();

            foreach (Module parent in parentModules)
            {
                parent.Nested = modulesRoles.Select(x => x.Module)
                    .Where(x => x.ParentId == parent.IdModule)
                    .OrderBy(x => x.Order)
                    .Select(x => new Module { IdModule = x.IdModule, Description = x.Description, AliasFlag = x.AliasFlag, Nested = x.Nested, UrlBase = x.UrlBase, Icon = x.Icon })
                    .ToList();
            }

            return parentModules;
        }
    }
}
