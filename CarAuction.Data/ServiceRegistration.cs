using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Brands;
using CarAuction.Data.Repositories.Categories;
using Microsoft.Extensions.DependencyInjection;
using CarAuction.Core.Repositories.Models;
using CarAuction.Core.Repositories.Tags;

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

            service.AddScoped<ITagWriteRepository, TagWriteRepository>();
            service.AddScoped<ITagReadRepository, TagReadRepository>();
        }
    }
}
