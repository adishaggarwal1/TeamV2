using ConceptArchitect.Data;
using ConceptArchitect.Utils;
using System.Data;

namespace ConceptArchitect.BookManagement.Repositories.Ado
{
    public class AdoAuthorRepository : IRepository<Author, string>
    {
        DbManager db;
        public AdoAuthorRepository(DbManager db)
        {
            this.db = db;
        }


        public async Task<Author> Add(Author author)
        {
            var query = $"insert into authors(id,name,biography,photo,email) " +
                              $"values('{author.Id}','{author.Name}','{author.Biography}','{author.Photo}','{author.Email}')";
            
            await db.ExecuteUpdateAsync(query);

            return author;
        }

        public async Task Delete(string id)
        {
            await db.ExecuteUpdateAsync($"delete from authors where id='{id}'");
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

        public async Task<List<Author>> GetAll()
        {
            return await db.QueryAsync("select * from authors", AuthorExtractor);
        }

        public async Task<List<Author>> GetAll(Func<Author, bool> predicate)
        {
            var authors = await GetAll();

            return (from author in authors
                   where predicate(author)
                   select author).ToList();
            
        }

        public async Task<Author> GetById(string id)
        {
            return await db.QueryOneAsync($"select * from authors where id='{id}'", AuthorExtractor);
        }

        public async Task<Author> Update(Author entity, Action<Author, Author> mergeOldNew)
        {
            var oldAuthor =await GetById(entity.Id);
            if(oldAuthor!=null)
            {
                mergeOldNew(oldAuthor, entity);
                var query = $"update authors set " +
                            $"name='{oldAuthor.Name}', " +
                            $"biography='{oldAuthor.Biography}', " +
                            $"photo='{oldAuthor.Photo}', " +
                            $"email='{oldAuthor.Email}' " +
                            $"where id='{oldAuthor.Id}'";

                await db.ExecuteUpdateAsync(query);
            }

            return entity;

            

        }
    }
}