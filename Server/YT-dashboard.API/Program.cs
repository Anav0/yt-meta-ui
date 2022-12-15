using YT.Data;

namespace YT.HTTP;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDbContext<PostgresContext>();

        var app = builder.Build();

        app.UseCors(builder =>
        {
            builder
                .WithOrigins("http://localhost:3000", "http://localhost:8000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .Build();
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.MapVideoEndpoints();

        app.Run();
    }
}
