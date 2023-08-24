using ConceptArchitect.Data;
using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.Ado
{
    public class AdoUserRepository : IRepository<User, string>
    {
        private DbManager db;

        public AdoUserRepository(DbManager db)
        {
            this.db = db;
        }

        private User UserExtractor(IDataReader reader)
        {
            return new User()
            {
                Email = reader["email"].ToString(),
                Name = reader["name"].ToString(),
                Profile_Photo = reader["profile_photo"].ToString()
            };
        }

        public async Task<User> Add(User user)
        {
            var query = $"insert into users(email,password,name,profile_photo) " +
                              $"values('{user.Email}','{user.Password}','{user.Name}','{user.Profile_Photo}')";

            await db.ExecuteUpdateAsync(query);

            return user;
        }

        public async Task<User> GetUser(string id)
        {
            return await db.QueryOneAsync($"select * from users where email='{id}'", UserExtractor);
        }

        public async Task<User> GetById(string id)
        {
            return await db.QueryOneAsync($"select * from users where email='{id}'", UserExtractor);
        }

        public async Task<List<User>> GetAll()
        {
            return await db.QueryAsync("select * from users", UserExtractor);
        }

        public async Task<List<User>> GetAll(Func<User, bool> predicate)
        {
            var users = await GetAll();

            return (from user in users
                    where predicate(user)
                    select user).ToList();
        }

        public Task<User> Update(User entity, Action<User, User> mergeOldNew)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}