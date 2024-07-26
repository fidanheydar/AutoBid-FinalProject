using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Colors;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Colors;
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
    public class ColorService : IColorService
    {
        private readonly IColorReadRepository _readRepository;
        private readonly IColorWriteRepository _writerepository;
        private readonly IMapper _mapper;

        public ColorService(IMapper mapper, IColorWriteRepository writerepository, IColorReadRepository readRepository)
        {
            _mapper = mapper;
            _writerepository = writerepository;
            _readRepository = readRepository;
        }

        public async Task<ApiResponse> CreateAsync(ColorPostDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Color color = _mapper.Map<Color>(dto);

          

            await _writerepository.AddAsync(color);
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = color
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Color> colors = await _readRepository.GetAll(x => !x.IsDeleted, count, page).ToListAsync();

            List<ColorGetDto> dtos = _mapper.Map<List<ColorGetDto>>(colors);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Color color = await _readRepository.GetByIdAsync(id,x => !x.IsDeleted,true);
            if (color is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            ColorGetDto dto = _mapper.Map<ColorGetDto>(color);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Color color = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (color is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            color.IsDeleted = true;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = color
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, ColorUpdateDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Color color = await _readRepository.GetByIdAsync(id,x =>!x.IsDeleted);
            if (color is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            _mapper.Map<ColorUpdateDto,Color>(dto,color);


            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = color
            };
        }
    }
}
