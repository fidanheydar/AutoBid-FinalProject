using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miles.Service.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryReadRepository _readRepository;
        private readonly ICategoryWriteRepository _writerepository;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, ICategoryWriteRepository writerepository, ICategoryReadRepository readRepository)
        {
            _mapper = mapper;
            _writerepository = writerepository;
            _readRepository = readRepository;
        }

        public async Task<ApiResponse> CreateAsync(CategoryPostDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Category category = _mapper.Map<Category>(dto);
            await _writerepository.AddAsync(category);
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = category
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Category> categories = await _readRepository.GetAll(x => !x.IsDeleted, count, page).ToListAsync();

            List<CategoryGetDto> dtos = _mapper.Map<List<CategoryGetDto>>(categories);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Category category = await _readRepository.GetByIdAsync(id,x => !x.IsDeleted);
            if (category is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            CategoryGetDto dto = _mapper.Map<CategoryGetDto>(category);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Category category = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (category is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            category.IsDeleted = true;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = category
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, CategoryUpdateDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Category category = await _readRepository.GetByIdAsync(id,x =>!x.IsDeleted);
            if (category is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            category.Name = dto.Name;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = category
            };
        }
    }
}
