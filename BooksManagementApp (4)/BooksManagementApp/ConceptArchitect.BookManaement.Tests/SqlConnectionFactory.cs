using ConceptArchitect.BookManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManaement.Tests
{
    internal class SqlConnectionFactory : IConnectionFactory
    {
        public string ConnectionString { get; set; }
        public IDbConnection CreateConnection()
        {
            var connection=new SqlConnection();
            connection.ConnectionString=ConnectionString;
            connection.Open();
            return connection;
        }
    }
}
