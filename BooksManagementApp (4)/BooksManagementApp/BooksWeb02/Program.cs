namespace BooksWeb02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.ConfigureMiddlewares();


            app.Run();
        }
    }
}