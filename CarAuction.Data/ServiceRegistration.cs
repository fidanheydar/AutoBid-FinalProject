using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Brands;
using Microsoft.Extensions.DependencyInjection;
using CarAuction.Data.Repositories.Categories;
using CarAuction.Core.Repositories.Models;
using CarAuction.Core.Repositories.Subscribes;
using CarAuction.Core.Repositories.Tags;
using CarAuction.Data.Repositories.Blogs;
using CarAuction.Core.Repositories.BlogTags;
using CarAuction.Core.Repositories.Fuels;
using CarAuction.Core.Repositories.Bans;
using CarAuction.Data.Repositories.Fuels;
using CarAuction.Data.Repositories.Subscribes;
using CarAuction.Core.Repositories.CarImages;
using CarAuction.Core.Repositories.Cars;
using CarAuction.Core.Repositories.Settings;
using CarAuction.Data.Repositories.Colors;
using CarAuction.Core.Repositories.Blogs;
using CarAuction.Core.Repositories.Colors;
using CarAuction.Data.Repositories.Cars;
using CarAuction.Data.Repositories.CarImages;
using CarAuction.Data.Repositories.BlogTags;
using CarAuction.Core.Repositories.Statuss;
using CarAuction.Core.Repositories.Bids;

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

            service.AddScoped<IBidWriteRepository, BidWriteRepository>();
            service.AddScoped<IBidReadRepository, BidReadRepository>();

            service.AddScoped<IBanWriteRepository, BanWriteRepository>();
            service.AddScoped<IBanReadRepository, BanReadRepository>();

            service.AddScoped<ICarImageWriteRepository, CarImageWriteRepository>();
            service.AddScoped<ICarImageReadRepository, CarImageReadRepository>();

            service.AddScoped<ICarWriteRepository, CarWriteRepository>();
            service.AddScoped<ICarReadRepository, CarReadRepository>();

            service.AddScoped<ISettingWriteRepository, SettingWriteRepository>();
            service.AddScoped<ISettingReadRepository, SettingReadRepository>();

            service.AddScoped<IColorWriteRepository, ColorWriteRepository>();
            service.AddScoped<IColorReadRepository, ColorReadRepository>();

            service.AddScoped<IStatusReadRepository, StatusReadRepository>();
            service.AddScoped<IStatusWriteRepository, StatusWriteRepository>();
        }
    }
}
