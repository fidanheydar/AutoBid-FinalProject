using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Tags;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Tags;
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
    public class TagService : ITagService
    {
        private readonly ITagReadRepository _readRepository;
        private readonly ITagWriteRepository _writerepository;
        private readonly IMapper _mapper;

        public TagService(IMapper mapper, ITagWriteRepository writerepository, ITagReadRepository readRepository)
        {
            _mapper = mapper;
            _writerepository = writerepository;
            _readRepository = readRepository;
        }

        public async Task<ApiResponse> CreateAsync(TagPostDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Tag tag = _mapper.Map<Tag>(dto);
            await _writerepository.AddAsync(tag);
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = tag
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Tag> tags = await _readRepository.GetAll(x => !x.IsDeleted, count, page).ToListAsync();

            List<TagGetDto> dtos = _mapper.Map<List<TagGetDto>>(tags);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Tag tag = await _readRepository.GetByIdAsync(id,x => !x.IsDeleted);
            if (tag is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            TagGetDto dto = _mapper.Map<TagGetDto>(tag);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Tag tag = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (tag is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            tag.IsDeleted = true;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = tag
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, TagUpdateDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && x.Id.ToString() != id))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Tag tag = await _readRepository.GetByIdAsync(id,x =>!x.IsDeleted);
            if (tag is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            tag.Name = dto.Name;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = tag
            };
        }
    }
}
