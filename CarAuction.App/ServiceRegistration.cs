

using CarAuction.App.Extensions;
using CarAuction.Core.Options;
using CarAuction.Data.Context;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CarAuction.App
{
    public static class ServiceRegistration
    {
        public static void AppServiceRegistration(this IServiceCollection service, IConfiguration configuration)
        {

            // Add services to the container.

            service.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            service.AddSwaggerExtension();
            service.AddHttpContextAccessor();


            service.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            service.Configure<MailSetting>(configuration.GetSection("MailSetting"));

            service.AddJWTTokenConfigurations(configuration["JwtTokenSettings:Audience"],
                                     configuration["JwtTokenSettings:Issuer"],
                                     configuration["JwtTokenSettings:SignInKey"]);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            service.AddEndpointsApiExplorer();

            service.AddDbContext<AuctionDbContext>(option =>
                    option.UseSqlServer(configuration.GetConnectionString("Develop"))
            );

            service.AddHangfire((sp, config) =>
            {
                var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("Develop");
                config.UseSqlServerStorage(connectionString);
            });
            service.AddHangfireServer();

        }
    }
}
