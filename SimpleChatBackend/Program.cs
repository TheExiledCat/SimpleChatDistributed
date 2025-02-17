using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using SimpleChatShared;

namespace SimpleChatBackend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers(o =>
        {
            o.Filters.Add<RequireLoginFilter>();
        });
        builder.Services.AddDbContext<ChatDbContext>(
            (config) =>
            {
                config.UseNpgsql(builder.Configuration.GetConnectionString("DevelopmentDb"));
            }
        );
        builder.Services.AddHttpLogging(
            (o) =>
            {
                o.LoggingFields =
                    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All
                    | HttpLoggingFields.RequestQuery;
            }
        );
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseHttpLogging();

            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
