using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BooksManagmentConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            IDbConnection connection = null;
            try
            {
                //step #1 create a connection
                connection = new SqlConnection();
                connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aggarad\Downloads\VivekProject\sql\BooksData.mdf;Integrated Security=True;Connect Timeout=30";
                connection.Open();

                //step #2 create a command
                IDbCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select * from authors";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var authorName = (string)reader["name"];
                    Console.WriteLine(authorName);

                }

            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }


            


        }
    }
}
