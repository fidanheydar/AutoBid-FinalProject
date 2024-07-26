using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Profiles
{
    public class EntityMapper:Profile
    {
        public EntityMapper()
        {
            CreateMap<Tag, TagPostDto>().ReverseMap();
            CreateMap<Tag, TagUpdateDto>().ReverseMap();
            CreateMap<TagGetDto, Tag>().ReverseMap();

            CreateMap<Category, CategoryPostDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<CategoryGetDto, Category>().ReverseMap();
        }
    }
}
