
using CarAuction.App.Middlewares;
using CarAuction.Data;
using CarAuction.Data.Context;
using CarAuction.Service;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.DataServiceRegistration();
            builder.Services.ServiceServiceRegistration();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<AuctionDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("Develop")),
                 ServiceLifetime.Transient
            );

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.CustomExceptionHadler();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
