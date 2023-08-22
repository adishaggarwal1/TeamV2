using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class AuthorManagerV1
    {
        //const string connectionString= @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\MyWorks\Corporate\202307-ecolab-cs\booksdb.mdf;Integrated Security = True; Connect Timeout = 30";

        public string ConnectionString { get; set; }

        public IConnectionFactory ConnectionFactory { get; set; }

        public AuthorManagerV1(IConnectionFactory factory)
        {
            ConnectionFactory = factory;
        }


        public List<Author> GetAllAuthors()
        {
            IDbConnection connection = null;
            var authors = new List<Author>();

            try
            {
                //connection = new SqlConnection();
                connection= ConnectionFactory.CreateConnection();
                //connection.ConnectionString = ConnectionString;
                //connection.Open();

                var command=connection.CreateCommand();
                command.CommandText = "select * from authors";

                var reader = command.ExecuteReader();
                

                while (reader.Read())
                {
                    var author = new Author()
                    {
                        Id = reader["id"].ToString(),
                        Name = reader["name"].ToString(),
                        Biography = reader["biography"].ToString(),
                        Photo = reader["photo"].ToString(),
                        Email = reader["email"].ToString()

                    };

                    authors.Add(author);

                }


                

            }catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                
            }
            finally
            {
                if (connection != null)
                    connection.Close();                
            }
            return authors;
        }

        

        public Author GetAuthorById(string id)
        {

            IDbConnection connection = null;
           

            try
            {
                //connection = new SqlConnection();
                //connection.ConnectionString = ConnectionString;
                //connection.Open();

                connection = ConnectionFactory.CreateConnection();

                var command = connection.CreateCommand();
                command.CommandText = $"select * from authors where id='{id}'";

                var reader = command.ExecuteReader();


                if (reader.Read())
                {
                    var author = new Author()
                    {
                        Id = reader["id"].ToString(),
                        Name = reader["name"].ToString(),
                        Biography = reader["biography"].ToString(),
                        Photo = reader["photo"].ToString(),
                        Email = reader["email"].ToString()

                    };

                    return author;

                }
                



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            throw new InvalidIdException<string>() { Id=id};
        }

        public int GetAuthorCount()
        {
            IDbConnection connection=null;
            try
            {
                //connection = new SqlConnection(ConnectionString);
                //connection.Open();

                connection = ConnectionFactory.CreateConnection();

                var command = connection.CreateCommand();

                command.CommandText = "select count(*) from authors";

                var count = (int) command.ExecuteScalar();
                return count;
            }            
            finally
            {
                if(connection!=null)
                    connection.Close();
            }
        }

        public List<Author> Search(string text)
        {
            IDbConnection connection = null;
            var result = new List<Author>();
            try
            {
                //connection =  new SqlConnection(ConnectionString);
                //connection.Open();
                connection = ConnectionFactory.CreateConnection();

                var command = connection.CreateCommand();

                command.CommandText = $"select * from authors where name like '%{text}%' or biography like '%{text}%'";

                var reader= command.ExecuteReader();
                while(reader.Read())
                {
                    var author = new Author()
                    {
                        Id = (string)reader["id"],
                        Name = (string)reader["name"],
                        Biography= (string)reader["biography"],
                        Email = reader["email"].ToString(),
                        Photo = reader["photo"].ToString()
                    };

                    result.Add(author); 
                }

                return result;
                
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }


        public void AddAuthor(Author author)
        {

            IDbConnection connection = null;
            try
            {
                //connection = new SqlConnection(ConnectionString);
                //connection.Open();
                connection = ConnectionFactory.CreateConnection();
                var command = connection.CreateCommand();

                command.CommandText = $"insert into authors(id,name,biography,photo,email) " +
                              $"values('{author.Id}','{author.Name}','{author.Biography}','{author.Photo}','{author.Email}')";

                var addCount = command.ExecuteNonQuery();
                

            }
            catch(Exception ex)
            {
                var expectedMessage = "Violation of PRIMARY KEY constraint";
                var expectedMessage2 = "The duplicate key value";

                if (ex.Message.Contains(expectedMessage) && ex.Message.Contains(expectedMessage2))
                    throw new DuplicateIdException<string>($"Duplicate Author Id {author.Id}") { Id = author.Id };
                else
                    throw;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        

        public void RemoveAuthor(string id)
        {
            IDbConnection connection = null;
            try
            {
                //connection = new SqlConnection(ConnectionString);
                //connection.Open();
                connection = ConnectionFactory.CreateConnection();

                var command = connection.CreateCommand();

                command.CommandText = $"delete from authors where id='{id}'";

                var deleteCount= command.ExecuteNonQuery();
                if (deleteCount == 0)
                    throw new InvalidIdException<string>() { Id = id };
                
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        
    }
}
