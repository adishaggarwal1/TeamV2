using ConceptArchitect.BookManagement;
using System.Data;

//class PostgrestConnectionFactory : IConnectionFactory
//{
//    private string connectionString;

//    public PostgrestConnectionFactory(string connectionString)
//    {
//        this.connectionString = connectionString;
//    }
//    public IDbConnection CreateConnection()
//    {
//        var connection = new Npgsql.NpgsqlConnection()
//        {
//            ConnectionString = connectionString
//        };

//        connection.Open();
//        return connection;
//    }
//}

class Program
{
    static void Main()
    {
        //var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\MyWorks\Corporate\202307-ecolab-cs\booksdb.mdf;Integrated Security=True;Connect Timeout=30";
       
        var manager = new AuthorManagerV2(() =>
        {
            var password = "22GbE2lDoF2v3HYG7L7o0Qoa4BBwUSBh";
            var userName = "dexynogt";
            var db = userName;
            var server = "john.db.elephantsql.com";
            var connectionString = $"Server={server};Userid={userName};Password={password};Database={db}";

            var connection = new Npgsql.NpgsqlConnection()
            {
                ConnectionString = connectionString
            };

            connection.Open();
            return connection;

        });
        


        foreach (Author author in manager.GetAllAuthors())
            Console.WriteLine(author);


    
    }
}
