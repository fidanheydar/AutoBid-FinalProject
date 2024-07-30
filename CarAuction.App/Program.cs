
using CarAuction.App.Extensions;
using CarAuction.App.Middlewares;
using CarAuction.Core.Options;
using CarAuction.Data;
using CarAuction.Data.Context;
using CarAuction.Service;
using CarAuction.Service.Services.Interfaces;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace CarAuction.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            builder.Services.DataServiceRegistration();
            builder.Services.ServiceServiceRegistration();

            builder.Services.AddSwaggerExtension();
            builder.Services.AddHttpContextAccessor();
            builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));

            builder.Services.AddJWTTokenConfigurations(builder.Configuration["JwtTokenSettings:Audience"],
                                     builder.Configuration["JwtTokenSettings:Issuer"],
                                     builder.Configuration["JwtTokenSettings:SignInKey"]);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<AuctionDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("Develop")),
                 ServiceLifetime.Transient
            );

            builder.Services.AddHangfire((sp, config) =>
            {
                var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("Develop");
                config.UseSqlServerStorage(connectionString);
            });
            builder.Services.AddHangfireServer();
           

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/api/swagger/admin_v1/swagger.json", "Admin API  V1");
                    x.SwaggerEndpoint("/api/swagger/user_v1/swagger.json", "User API V1");
                    x.RoutePrefix = "api/swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");

            app.CustomExceptionHadler();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseHangfireDashboard();
            app.MapControllers();

            app.Run();
        }
    }
}
