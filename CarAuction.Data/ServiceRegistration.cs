using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Brands;
using CarAuction.Data.Repositories.Categories;
using Microsoft.Extensions.DependencyInjection;
using CarAuction.Core.Repositories.Models;
using CarAuction.Core.Repositories.Subscribes;
using CarAuction.Core.Repositories.Tags;
using CarAuction.Core.Repositories.Blogs;
using CarAuction.Data.Repositories.Blogs;
using CarAuction.Core.Repositories.BlogTags;
using CarAuction.Data.Repositories.BlogTags;
using CarAuction.Core.Repositories.Fuels;
using CarAuction.Core.Repositories.Bans;
using CarAuction.Data.Repositories.Fuels;
using CarAuction.Data.Repositories.Subscribes;

namespace CarAuction.Data
{
    public static class ServiceRegistration
    {
        public static void DataServiceRegistration(this IServiceCollection service)
        {
            service.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            service.AddScoped<ICategoryReadRepository, CategoryReadRepository>();

            service.AddScoped<IModelWriteRepository, ModelWriteRepository>();
            service.AddScoped<IModelReadRepository, ModelReadRepository>();

            service.AddScoped<IBrandWriteRepository,BrandWriteRepository>();
            service.AddScoped<IBrandReadRepository,BrandReadRepository>();

            service.AddScoped<ISubscribeWriteRepository, SubscribeWriteRepository>();
            service.AddScoped<ISubscribeReadRepository, SubscribeReadRepository>();

            service.AddScoped<IBlogWriteRepository, BlogWriteRepository>();
            service.AddScoped<IBlogReadRepository, BlogReadRepository>();

            service.AddScoped<IBlogTagWriteRepository, BlogTagWriteRepository>();
            service.AddScoped<IBlogTagReadRepository, BlogTagReadRepository>();

            service.AddScoped<ITagWriteRepository, TagWriteRepository>();
            service.AddScoped<ITagReadRepository, TagReadRepository>();

            service.AddScoped<IFuelWriteRepository, FuelWriteRepository>();
            service.AddScoped<IFuelReadRepository, FuelReadRepository>();

            service.AddScoped<IBanWriteRepository, BanWriteRepository>();
            service.AddScoped<IBanReadRepository, BanReadRepository>();
        }
    }
}
