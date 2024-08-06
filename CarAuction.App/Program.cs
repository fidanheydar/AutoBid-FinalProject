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
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Text.Json.Serialization;

namespace CarAuction.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AppServiceRegistration(builder.Configuration);
            builder.Services.AddServiceRegistration();
            builder.Services.AddDataRegistration();

            builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

            //builder.Host.AddSerilogExtension(builder.Configuration.GetConnectionString("Develop"));

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
