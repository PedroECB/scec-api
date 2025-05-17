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
    public class LogAcessRepository : IRepository<LogAcess>
    {
        private DataContext _context;

        public LogAcessRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<LogAcess> Add(LogAcess log)
        {
            _context.LogAcess.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public Task<LogAcess> Delete(LogAcess entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LogAcess>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<LogAcess> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LogAcess> Update(LogAcess entity)
        {
            throw new NotImplementedException();
        }
    }
}
