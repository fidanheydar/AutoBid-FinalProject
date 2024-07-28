using CarAuction.Data;
using CarAuction.Data.Context;
using CarAuction.MVC.Middlewares;
using CarAuction.Service;
using CarAuction.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarAuction.MVC
{
    public class Program
    {
        public static void Main(string[] args)
		{
            var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<AuctionDbContext>(opt =>
			{
				opt.UseSqlServer(builder.Configuration.GetConnectionString("Develop"));
			});
			// Add services to the container.
			builder.Services.AddControllersWithViews();
            builder.Services.ServiceServiceRegistration();
            builder.Services.DataServiceRegistration();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.CustomExceptionHadler();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
