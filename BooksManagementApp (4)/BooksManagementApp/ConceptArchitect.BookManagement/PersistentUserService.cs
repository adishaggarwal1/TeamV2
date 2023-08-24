using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class PersistentUserService : IUserService
    {
        private IRepository<User, string> repository;

        public PersistentUserService(IRepository<User, string> repository)
        {
            this.repository = repository;
        }

        public async Task<User> Add(User user)
        {
            return await repository.Add(user);
        }

        public async Task<User> GetUser(string email)
        {
            return await repository.GetById(email);
        }
    }
}