using ConceptArchitect.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class AuthorManager
    {

        DbManager db;

        public AuthorManager(DbManager manager)
        {
            db = manager;
        }

        private Author AuthorExtractor(IDataReader reader)
        {
            return new Author()
            {
                Id = reader["id"].ToString(),
                Name = reader["name"].ToString(),
                Biography = reader["biography"].ToString(),
                Photo = reader["photo"].ToString(),
                Email = reader["email"].ToString()

            };
        }


        public List<Author> GetAllAuthors()
        {
            return db.Query("select * from authors", AuthorExtractor);            
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await db.QueryAsync("select * from authors", AuthorExtractor);
        }



        public Author GetAuthorById(string id)
        {
            return db.QueryOne($"select * from authors where id='{id}'", AuthorExtractor);
        }

        public int GetAuthorCount()
        {
            return db.QueryScalar<int>("select count(*) from authors");
        }

        public List<Author> Search(string text, int skip=0, int take=0)
        {
            var query= $"select * from authors where name like '%{text}%' or biography like '%{text}%'";

            return db.Query(query, AuthorExtractor, skip, take);
        }


        public void AddAuthor(Author author)
        {
            var query = $"insert into authors(id,name,biography,photo,email) " +
                              $"values('{author.Id}','{author.Name}','{author.Biography}','{author.Photo}','{author.Email}')";

            try
            {
                db.ExecuteUpdate(query);
            }
            catch (Exception ex)
            {
                var expectedMessage = "Violation of PRIMARY KEY constraint";
                var expectedMessage2 = "The duplicate key value";

                if (ex.Message.Contains(expectedMessage) && ex.Message.Contains(expectedMessage2))
                    throw new DuplicateIdException<string>($"Duplicate Author Id {author.Id}") { Id = author.Id };
                else
                    throw;
            }
            
        }



        public void RemoveAuthor(string id)
        {
                var deleteCount = db.ExecuteUpdate($"delete from authors where id='{id}'");

                if (deleteCount == 0)
                    throw new InvalidIdException<string>() { Id = id };

        }


    }
}
