using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Fuels;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Fuels;
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
    public class FuelService : IFuelService
    {
        private readonly IFuelReadRepository _readRepository;
        private readonly IFuelWriteRepository _writerepository;
        private readonly IMapper _mapper;

        public FuelService(IMapper mapper, IFuelWriteRepository writerepository, IFuelReadRepository readRepository)
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

        public async Task<ApiResponse> CreateAsync(FuelPostDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Fuel brand = _mapper.Map<Fuel>(dto);
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
            IEnumerable<Fuel> brands = await _readRepository.GetAll(x => !x.IsDeleted, count, page).ToListAsync();

            List<FuelGetDto> dtos = _mapper.Map<List<FuelGetDto>>(brands);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Fuel brand = await _readRepository.GetByIdAsync(id,x => !x.IsDeleted,true);
            if (brand is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            FuelGetDto dto = _mapper.Map<FuelGetDto>(brand);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Fuel brand = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
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

        public async Task<ApiResponse> UpdateAsync(string id, FuelUpdateDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && x.Id.ToString() != id))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Fuel brand = await _readRepository.GetByIdAsync(id,x =>!x.IsDeleted);
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
