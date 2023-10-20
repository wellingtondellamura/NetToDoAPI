using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Auth;

namespace Api;



public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.

        // Add injeção de dependência do Auth token service
        builder.Services.AddTransient<AuthService>();
        builder.Services.AddTransient<CryptService>();


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApiDbContext>(options =>
             options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnectionString")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
