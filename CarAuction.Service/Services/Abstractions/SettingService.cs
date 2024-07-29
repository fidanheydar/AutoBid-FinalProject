using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Settings;
using CarAuction.Service.DTOs.Settings;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using CarAuction.Service.Extensions;

namespace Miles.Service.Services.Implementations
{
    public class SettingService : ISettingService
    {
        private readonly ISettingReadRepository _readRepository;
        private readonly ISettingWriteRepository _writerepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;
        public SettingService(IMapper mapper, ISettingWriteRepository writerepository, ISettingReadRepository readRepository, IHttpContextAccessor http, IWebHostEnvironment evn)
        {
            _mapper = mapper;
            _writerepository = writerepository;
            _readRepository = readRepository;
            _http = http;
            _evn = evn;
        }

        public async Task<ApiResponse> CreateAsync(SettingPostDto dto)
        {

            Setting Setting = _mapper.Map<Setting>(dto);

            string url = dto.Logo.CreateImage(_evn.WebRootPath, "Images/Settings");
            Setting.LogoUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"/Images/Settings/{url}";

            url = dto.AboutImage.CreateImage(_evn.WebRootPath, "Images/Settings");
            Setting.AboutImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"/Images/Settings/{url}";

            await _writerepository.AddAsync(Setting);
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Setting
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count, int page)
        {
            IEnumerable<Setting> Settings = await _readRepository.GetAll(x => !x.IsDeleted, count, page).ToListAsync();

            List<SettingGetDto> dtos = _mapper.Map<List<SettingGetDto>>(Settings);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Setting Setting = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted, true);

            if (Setting is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            SettingGetDto dto = _mapper.Map<SettingGetDto>(Setting);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Setting Setting = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (Setting is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Setting.IsDeleted = true;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Setting
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, SettingUpdateDto dto)
        {
            Setting Setting = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (Setting is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }

            Setting = _mapper.Map<SettingUpdateDto, Setting>(dto, Setting);

            if (dto.Logo != null)
            {
                string url = dto.Logo.CreateImage(_evn.WebRootPath, "Images/Settings");
                Setting.LogoUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"/Images/Settings/{url}";
            }
            if (dto.AboutImage != null)
            {
                string url = dto.AboutImage.CreateImage(_evn.WebRootPath, "Images/Settings");
                Setting.AboutImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                    + $"/Images/Settings/{url}";
            }

            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Setting
            };
        }
    }
}
