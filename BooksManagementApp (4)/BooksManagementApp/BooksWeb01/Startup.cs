using ConceptArchitect.BookManagement;
using ConceptArchitect.Data;
using Microsoft.Data.SqlClient;

namespace BooksWeb01
{
    public static class Startup
    {
        public static void ConfigureMiddelwares(this IApplicationBuilder app)
        {
            app.UseOnUrl("/greet", async context => await context.Response.WriteAsync("Hello World"));

            app.UseOnUrl("/authors", async context =>
            {
                var db = new DbManager(() =>
                {
                    var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\MyWorks\Corporate\202307-ecolab-cs\booksdb3.mdf;Integrated Security=True;Connect Timeout=30";
                    var con = new SqlConnection(connectionString);
                    con.Open();
                    return con;
                });

                var authorManager = new AuthorManager(db);

                var authors = authorManager.GetAllAuthors();

                await context.Response.WriteAsync("<h1>List of All Authors</h1>");
                await context.Response.WriteAsync("<ul>");
                foreach (var author in authors)
                    await context.Response.WriteAsync($"<li><a href='/authors/{author.Id}'>{author.Name}</a></li>");
                await context.Response.WriteAsync("</ul>");
            });

            app.UseOnUrl("/authors", async context =>
            {
                var paths = context.Request.Path.ToString().Split("/");
                var id = paths[2];
                var db = new DbManager(() =>
                {
                    var connection = new SqlConnection()
                    {
                        ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\MyWorks\Corporate\202307-ecolab-cs\booksdb3.mdf;Integrated Security=True;Connect Timeout=30"
                    };
                    connection.Open();
                    return connection;
                });

                var authorManager = new AuthorManager(db);

                var author = authorManager.GetAuthorById(id);

                await context.Response.WriteAsync($"<h1>About {author.Name}</h1>");
                await context.Response.WriteAsync($"<p><img src='{author.Photo}' width='300' alt='{author.Name}'/>");
                await context.Response.WriteAsync($"{author.Biography}</p>");

            }, false);

        }
    }
}
