using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Bans;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Bans;
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

namespace Miles.Service.Services.Implementations
{
    public class BanService : IBanService
    {
        private readonly IBanReadRepository _readRepository;
        private readonly IBanWriteRepository _writerepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;

        public BanService(IMapper mapper, IBanWriteRepository writerepository, IBanReadRepository readRepository, IHttpContextAccessor http, IWebHostEnvironment evn)
        {
            _mapper = mapper;
            _writerepository = writerepository;
            _readRepository = readRepository;
            _http = http;
            _evn = evn;
        }

        public async Task<ApiResponse> CreateAsync(BanPostDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Ban ban = _mapper.Map<Ban>(dto);

            string url = dto.Image.CreateImage(_evn.WebRootPath, "Images/Bans");
            ban.ImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"/Images/Bans/{url}";

            await _writerepository.AddAsync(ban);
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = ban
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Ban> bans = await _readRepository.GetAll(x => !x.IsDeleted, count, page).ToListAsync();

            List<BanGetDto> dtos = _mapper.Map<List<BanGetDto>>(bans);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Ban ban = await _readRepository.GetByIdAsync(id,x => !x.IsDeleted,true);
            if (ban is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            BanGetDto dto = _mapper.Map<BanGetDto>(ban);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Ban ban = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (ban is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            ban.IsDeleted = true;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = ban
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, BanUpdateDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Ban ban = await _readRepository.GetByIdAsync(id,x =>!x.IsDeleted);
            if (ban is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            if (dto.Image != null)
            {
                string url = dto.Image.CreateImage(_evn.WebRootPath, "Images/Bans");
                ban.ImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"/Images/Bans/{url}";
            }
            ban.Name = dto.Name;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = ban
            };
        }
    }
}
