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
using CarAuction.Service.Services.Abstractions;
using System.Reflection.Metadata;

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
        private readonly IIdentityService _identityService;
        public BlogService(IMapper mapper, IBlogWriteRepository writerepository, IBlogReadRepository readRepository, IHttpContextAccessor http, IWebHostEnvironment evn, IBlogTagWriteRepository tagWriteRepository, IBlogTagReadRepository tagReadRepository, IIdentityService identityService)
        {
            _mapper = mapper;
            _writerepository = writerepository;
            _readRepository = readRepository;
            _http = http;
            _evn = evn;
            _tagWriteRepository = tagWriteRepository;
            _tagReadRepository = tagReadRepository;
            _identityService = identityService;
        }

        public async Task<ApiResponse> CreateAsync(BlogPostDto dto)
        {
            string userName = _http.HttpContext?.User.Identity.Name;
            var user = await _identityService.GetUserByName(userName);
            if (user == null)
                throw new Exception("User not found");


            Blog Blog = _mapper.Map<Blog>(dto);

            Blog.AdminId = user.Id;

            string url = dto.BaseImage.CreateImage(_evn.WebRootPath, "Images/Blogs");
            Blog.BaseImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"/Images/Blogs/{url}";

            url = dto.SectionImage.CreateImage(_evn.WebRootPath, "Images/Blogs");
            Blog.SectionImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"/Images/Blogs/{url}";

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
            IEnumerable<Blog> Blogs = await _readRepository.GetAll(x => !x.IsDeleted, count, page).Include(x => x.Category).Include(x => x.Admin).Include(x => x.Comments).ThenInclude(x => x.User).Include(x => x.BlogTags).ThenInclude(x => x.Tag).ToListAsync();

            List<BlogGetDto> dtos = _mapper.Map<List<BlogGetDto>>(Blogs);

            foreach (BlogGetDto dto in dtos)
            {
                dto.Tags = _mapper.Map<List<TagGetDto>>(_tagReadRepository.GetAll(x => !x.IsDeleted && x.BlogId == dto.Id, count, page).Include(x => x.Tag).ToList().Select(x => x.Tag).ToList());

            }

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Blog? Blog = await _readRepository.GetAll(x => !x.IsDeleted && x.Id.ToString() == id, 0, 0).Include(x => x.Category).Include(x => x.Admin).Include(x => x.BlogTags).Include(x => x.Comments).ThenInclude(x => x.User).FirstOrDefaultAsync();

            if (Blog is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }

            Blog.BlogTags = await (_tagReadRepository.GetAll(x => x.BlogId == Blog.Id, 0, 0)).Include(x => x.Tag).ToListAsync();

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
            Blog Blog = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted, true, "BlogTags");
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
                    + $"/Images/Blogs/{url}";
            }
            if (dto.SectionImage != null)
            {
                string url = dto.SectionImage.CreateImage(_evn.WebRootPath, "Images/Blogs");
                Blog.SectionImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"/Images/Blogs/{url}";
            }
            Blog.CategoryId = dto.CategoryId;
            Blog.Description = dto.Description;
            Blog.Title = dto.Title;
            Blog.Fact = dto.Fact;

            var existingTags = Blog.BlogTags.Select(bt => bt.TagId).ToList();

            // Remove tags that are no longer in the dto.TagIds
            var removableTags = Blog.BlogTags.Where(bt => !dto.TagIds.Contains(bt.TagId)).ToList();
            _tagWriteRepository.RemoveRange(removableTags);

            // Add new tags that are in dto.TagIds but not in the existing tags
            var newTags = dto.TagIds.Where(tagId => !existingTags.Contains(tagId)).Select(tagId => new BlogTag
            {
                BlogId = Blog.Id,
                TagId = tagId,
                Id = Guid.NewGuid()
            }).ToList();
            await _tagWriteRepository.AddRangeAsync(newTags);

            await _writerepository.SaveAsync();

            return new ApiResponse
            {
                StatusCode = 200,
                items = Blog
            };
        }
    }
}
