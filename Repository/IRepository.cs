using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCEC.API.Repository
{
    interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
