using ConceptArchitect.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    

    public class BookManager
    {
        DbManager db;
        public BookManager(DbManager db)
        {
            this.db = db;
        }

        public Book BookExtractor(IDataReader reader)
        {
            return new Book()
            {
                Id = reader["id"].ToString(),
                Title = reader["title"].ToString(),
                Description = reader["description"].ToString(),
                Author_Id = reader["author_id"].ToString(),
                Cover_Photo = reader["cover_photo"].ToString()
            };
        }


        public List<Book> GetAllBooks()
        {
            return db.Query("select * from books", BookExtractor);
        }

        public Book GetBookById(string id)
        {
            return db.QueryOne($"select * from authors where id='{id}'", BookExtractor);
        }
    }
}
