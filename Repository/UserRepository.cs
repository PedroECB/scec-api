using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCEC.API.Models;

namespace SCEC.API.Repository
{
    public class UserRepository : IRepository<User>
    {

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

        public IEnumerable<User> GetAll()
        {
            Random randomGenerator = new Random();
            List<User> users = new List<User>();
            users.Add(new User() { Id = randomGenerator.Next(100), Name = "Pedro Henrique", BirthDate = DateTime.Now });
            users.Add(new User() { Id = randomGenerator.Next(100), Name = "Paulo Silveira", BirthDate = DateTime.Now });
            users.Add(new User() { Id = randomGenerator.Next(100), Name = "Silva Martins", BirthDate = DateTime.Now });

            return users;
        }


        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
