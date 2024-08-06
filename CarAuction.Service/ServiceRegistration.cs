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
        public static void AddServiceRegistration(this IServiceCollection service)
        {
            service.AddControllers()?.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<CategoryPostDto>());

            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IBanService, BanService>();
            service.AddScoped<IBrandService, BrandService>();
            service.AddScoped<IModelService, ModelService>();
            service.AddScoped<ITagService, TagService>();
            service.AddScoped<IBlogService, BlogService>();
            service.AddScoped<IFuelService, FuelService>();
            service.AddScoped<ISubscribeService, SubscribeService>();
            service.AddScoped<ICarService, CarService>();
            service.AddScoped<IColorService, ColorService>();
            service.AddScoped<IMailService, MailService>();
            service.AddScoped<ITokenService, TokenService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IIdentityService, IdentityService>();
            service.AddScoped<ISettingService, SettingService>();
            service.AddScoped<IStatusService, StatusService>();
            service.AddScoped<IBidService, BidService>();
            service.AddScoped<IAuctionService, AuctionService>();
            service.AddScoped<ICommentService, CommentService>();

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
               options.SignIn.RequireConfirmedEmail = true;
           }).AddEntityFrameworkStores<AuctionDbContext>().AddDefaultTokenProviders();

        }
    }
}
