using CarAuction.Core.Models;
using CarAuction.Data.Context;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.Services.Abstractions;
using CarAuction.Service.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Miles.Service.Services.Implementations;

namespace CarAuction.Service
{
    public static class ServiceRegistration
    {
        public static void ServiceServiceRegistration(this IServiceCollection service)
        {
            service.AddControllers()?.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<CategoryPostDto>());

            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<IBanService, BanService>();
            service.AddTransient<IBrandService, BrandService>();
            service.AddTransient<IModelService, ModelService>();
            service.AddTransient<ITagService, TagService>();
            service.AddTransient<IBlogService, BlogService>();
            service.AddTransient<IFuelService, FuelService>();
            service.AddTransient<ISubscribeService, SubscribeService>();
            service.AddTransient<ICarService, CarService>();
            service.AddTransient<IColorService, ColorService>();
            service.AddTransient<IMailService, MailService>();
            service.AddTransient<ITokenService, TokenService>();
            service.AddTransient<IAuthService, AuthService>();
            service.AddTransient<IIdentityService, IdentityService>();
            service.AddTransient<ISettingService, SettingService>();
            service.AddTransient<IStatusService, StatusService>();
            service.AddTransient<IBidService, BidService>();


            service.AddIdentity<AppUser, AppRole>(
           options =>
           {
               options.Password.RequiredLength = 3;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireDigit = true;
               options.Password.RequireLowercase = true;
               options.Password.RequireUppercase = true;
               options.Lockout.AllowedForNewUsers = false;
               options.User.RequireUniqueEmail = true;
           }).AddEntityFrameworkStores<AuctionDbContext>().AddDefaultTokenProviders();

        }
    }
}
