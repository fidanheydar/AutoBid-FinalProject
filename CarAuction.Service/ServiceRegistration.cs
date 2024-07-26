using AutoMapper;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Data.Repositories.Categories;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.Services.Abstractions;
using CarAuction.Service.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Miles.Service.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service
{
    public static class ServiceRegistration
    {
        public static void ServiceServiceRegistration(this IServiceCollection service)
        {
            service.AddControllers()?.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<CategoryPostDto>());

            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            service.AddScoped<ICategoryService,CategoryService>();
            service.AddScoped<IBanService,BanService>();
            service.AddScoped<IBrandService, BrandService>();
            service.AddScoped<IModelService, ModelService>();
            service.AddScoped<ITagService, TagService>();
            service.AddScoped<IBlogService, BlogService>();
            service.AddScoped<IFuelService,FuelService>();
            service.AddScoped<ISubscribeService, SubscribeService>();
         
        }
    }
}
