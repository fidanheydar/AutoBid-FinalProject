using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Statuss;
using CarAuction.Service.DTOs.Statuses;
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
    public class StatusService : IStatusService
    {
        private readonly IStatusReadRepository _readRepository;
        private readonly IStatusWriteRepository _writerepository;
        private readonly IMapper _mapper;

        public StatusService(IMapper mapper, IStatusWriteRepository writerepository, IStatusReadRepository readRepository)
        {
            _mapper = mapper;
            _writerepository = writerepository;
            _readRepository = readRepository;
        }

        public async Task<ApiResponse> CreateAsync(StatusPostDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Status Status = _mapper.Map<Status>(dto);
            await _writerepository.AddAsync(Status);
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Status
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Status> Statuss = await _readRepository.GetAll(x => !x.IsDeleted, count, page).Include(x=>x.Cars).ToListAsync();

            List<StatusGetDto> dtos = _mapper.Map<List<StatusGetDto>>(Statuss);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            Status Status = await _readRepository.GetByIdAsync(id,x => !x.IsDeleted,true,"Cars");
            if (Status is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            StatusGetDto dto = _mapper.Map<StatusGetDto>(Status);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Status Status = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (Status is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Status.IsDeleted = true;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Status
            };
        }

        public async Task<ApiResponse> UpdateAsync(string id, StatusUpdateDto dto)
        {
            if (await _readRepository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && id != x.Id.ToString()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Status Status = await _readRepository.GetByIdAsync(id,x =>!x.IsDeleted);
            if (Status is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Status.Name = dto.Name;
            Status.Level = dto.Level;
            await _writerepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Status
            };
        }
    }
}
