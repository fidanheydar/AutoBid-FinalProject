using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Brands;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Brands;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.Profiles;

namespace Miles.Service.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandReadRepository _readRepository;
        private readonly IBrandWriteRepository _writerepository;
        private readonly IMapper _mapper;

        public BrandService(IMapper mapper, IBrandWriteRepository writerepository, IBrandReadRepository readRepository)
        {
            _mapper = mapper;
            _writerepository = writerepository;
            _readRepository = readRepository;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityMapper());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        public async Task<ApiResponse> CreateAsync(BrandPostDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Brand brand = _mapper.Map<Brand>(dto);
            await _writerepository.AddAsync(brand);
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = brand
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Brand> brands = await _readRepository.GetAll(x => !x.IsDeleted, count, page).Include(x=>x.Models).ToListAsync();

            

            List<BrandGetDto> dtos = _mapper.Map<List<BrandGetDto>>(brands);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Brand brand = await _readRepository.GetByIdAsync(id,x => !x.IsDeleted,true,"Models");
            if (brand is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            BrandGetDto dto = _mapper.Map<BrandGetDto>(brand);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Brand brand = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (brand is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            brand.IsDeleted = true;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = brand
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, BrandUpdateDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Brand brand = await _readRepository.GetByIdAsync(id,x =>!x.IsDeleted);
            if (brand is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            brand.Name = dto.Name;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = brand
            };
        }
    }
}
