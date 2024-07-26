using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Models;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Models;
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
    public class ModelService : IModelService
    {
        private readonly IModelReadRepository _readRepository;
        private readonly IModelWriteRepository _writerepository;
        private readonly IMapper _mapper;

        public ModelService(IMapper mapper, IModelWriteRepository writerepository, IModelReadRepository readRepository)
        {
            _mapper = mapper;
            _writerepository = writerepository;
            _readRepository = readRepository;
        }

        public async Task<ApiResponse> CreateAsync(ModelPostDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Model model = _mapper.Map<Model>(dto);
            await _writerepository.AddAsync(model);
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = model
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Model> models = await _readRepository.GetAll(x => !x.IsDeleted, count, page).Include(x=>x.Brand).ToListAsync();

            List<ModelGetDto> dtos = _mapper.Map<List<ModelGetDto>>(models);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Model model = await _readRepository.GetByIdAsync(id,x => !x.IsDeleted,true,"Brand");
            if (model is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            ModelGetDto dto = _mapper.Map<ModelGetDto>(model);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Model model = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (model is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            model.IsDeleted = true;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = model
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, ModelUpdateDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && x.BrandId == dto.BrandId))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Model model = await _readRepository.GetByIdAsync(id,x =>!x.IsDeleted);
            if (model is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            model.Name = dto.Name;
            model.BrandId = dto.BrandId;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = model
            };
        }
    }
}
