using ConceptArchitect.BookManagement;
using ConceptArchitect.BookManagement.Repositories.Ado;
using ConceptArchitect.Data;
using ConceptArchitect.Utils;
using System.Data;
using System.Data.SqlClient;

namespace BooksWeb02.Extensions
{
    public static class AdoRepositoryExtensions
    {
        public static IServiceCollection AddAdoBMSRepository(this IServiceCollection services)
        {
            services.AddSingleton<Func<IDbConnection>>(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                var connectionString = config.GetConnectionString("bms");

                return () =>
                {
                    var connection = new SqlConnection();
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    return connection;
                };
            });

            services.AddSingleton<DbManager>();

            //add all your repositories here  AuthorRepository, BookRepository

            services.AddSingleton<IRepository<Author, string>, AdoAuthorRepository>();
            services.AddSingleton<IRepository<Book, string>, AdoBookRepository>();


            return services;
        }

    }
}
