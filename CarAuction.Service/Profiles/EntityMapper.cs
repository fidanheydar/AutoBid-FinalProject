using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAuction.Service.DTOs.Models;
using CarAuction.Service.DTOs.Blogs;
using CarAuction.Service.DTOs.Brands;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.DTOs.Fuels;

namespace CarAuction.Service.Profiles
{
    public class EntityMapper:Profile
    {
        public EntityMapper()
        {
            CreateMap<Blog, BlogPostDto>().ReverseMap();
            CreateMap<Blog, BlogUpdateDto>().ReverseMap();
            CreateMap<BlogGetDto, Blog>().ReverseMap();

            CreateMap<Brand, BrandPostDto>().ReverseMap();
            CreateMap<Brand, BrandUpdateDto>().ReverseMap();
            CreateMap<BrandGetDto, Brand>().ReverseMap();

            CreateMap<Model, ModelPostDto>().ReverseMap();
            CreateMap<Model, ModelUpdateDto>().ReverseMap();
            CreateMap<ModelGetDto, Model>().ReverseMap();

            CreateMap<Fuel, FuelPostDto>().ReverseMap();
            CreateMap<Fuel, FuelUpdateDto>().ReverseMap();
            CreateMap<FuelGetDto, Fuel>().ReverseMap();

            CreateMap<Blog, BlogPostDto>().ReverseMap();
            CreateMap<Blog, BlogUpdateDto>().ReverseMap();
            CreateMap<BlogGetDto, Blog>().ReverseMap();

            CreateMap<Tag, TagPostDto>().ReverseMap();
            CreateMap<Tag, TagUpdateDto>().ReverseMap();
            CreateMap<TagGetDto, Tag>().ReverseMap();

            CreateMap<Category, CategoryPostDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<CategoryGetDto, Category>().ReverseMap();

        }
    }
}
