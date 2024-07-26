using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Blogs;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Blogs;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAuction.Service.DTOs.Tags;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Metrics;
using CarAuction.Service.Extensions;
using CarAuction.Core.Repositories.BlogTags;
using CarAuction.Core.Repositories.Tags;

namespace Miles.Service.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogReadRepository _readRepository;
        private readonly IBlogWriteRepository _writerepository;
        private readonly IBlogTagWriteRepository _tagWriteRepository;
        private readonly IBlogTagReadRepository _tagReadRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;

        public BlogService(IMapper mapper, IBlogWriteRepository writerepository, IBlogReadRepository readRepository, IHttpContextAccessor http, IWebHostEnvironment evn, IBlogTagWriteRepository tagWriteRepository, IBlogTagReadRepository tagReadRepository)
        {
            _mapper = mapper;
            _writerepository = writerepository;
            _readRepository = readRepository;
            _http = http;
            _evn = evn;
            _tagWriteRepository = tagWriteRepository;
            _tagReadRepository = tagReadRepository;
        }

        public async Task<ApiResponse> CreateAsync(BlogPostDto dto)
        {

            Blog Blog = _mapper.Map<Blog>(dto);

            string url = dto.BaseImage.CreateImage(_evn.WebRootPath, "Images/Blogs");
            Blog.BaseImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"Images/Blogs/{url}";

            url = dto.SectionImage.CreateImage(_evn.WebRootPath, "Images/Blogs");
            Blog.SectionImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"Images/Blogs/{url}";

            foreach (var item in dto.TagIds)
            {
                Blog.BlogTags.Add(new()
                {
                    BlogId = Blog.Id,
                    TagId = item,
                    Id = Guid.NewGuid(),
                });
            }

            await _writerepository.AddAsync(Blog);
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Blog
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count, int page)
        {
            IEnumerable<Blog> Blogs = await _readRepository.GetAll(x => !x.IsDeleted, count, page).Include(x => x.Category).Include(x => x.BlogTags).ThenInclude(x => x.Tag).ToListAsync();

            List<BlogGetDto> dtos = _mapper.Map<List<BlogGetDto>>(Blogs);

            foreach (BlogGetDto dto in dtos)
            {
                dto.Tags =  _tagReadRepository.GetAll(x=>!x.IsDeleted && x.BlogId == dto.Id, count, page).Include(x=>x.Tag).ToList().Select(x=>x.Tag).ToList();
            }

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Blog Blog = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted, true);
            if (Blog is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            BlogGetDto dto = _mapper.Map<BlogGetDto>(Blog);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Blog Blog = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (Blog is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Blog.IsDeleted = true;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Blog
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, BlogUpdateDto dto)
        {
            Blog Blog = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (Blog is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (dto.BaseImage != null)
            {
                string url = dto.BaseImage.CreateImage(_evn.WebRootPath, "Images/Blogs");
                Blog.BaseImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"Images/Blogs/{url}";
            }
            if (dto.SectionImage != null)
            {
                string url = dto.SectionImage.CreateImage(_evn.WebRootPath, "Images/Blogs");
                Blog.SectionImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"Images/Blogs/{url}";
            }
            Blog.CategoryId = dto.CategoryId;
            Blog.Description = dto.Description;
            Blog.Title = dto.Title;
            Blog.Fact = dto.Fact;

            List<BlogTag> RemoveableTags = await _tagReadRepository.GetAll(x => !dto.TagIds.Contains(x.TagId) && x.BlogId == Blog.Id,0,0).ToListAsync();


            _tagWriteRepository.RemoveRange(RemoveableTags);
            await _tagWriteRepository.SaveAsync();

            foreach (var item in dto.TagIds)
            {
                if (_tagReadRepository.GetAll(x => x.BlogId.ToString() == id && x.TagId == item,0,0).Count() > 0)
                    continue;

                Blog.BlogTags.Add(new()
                {
                    BlogId = Blog.Id,
                    TagId = item,
                    Id = Guid.NewGuid(),
                });
            }


            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Blog
            };
        }
    }
}
