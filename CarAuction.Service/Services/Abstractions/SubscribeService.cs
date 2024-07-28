using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Subscribes;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Abstractions
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscribeReadRepository _readRepository;
        private readonly ISubscribeWriteRepository _writeRepository;

        public SubscribeService(ISubscribeWriteRepository writeRepository, ISubscribeReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<ApiResponse> CreateAsync(string email)
        {

            Regex re = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            if (!re.IsMatch(email))
            {
                return new ApiResponse()
                {
                    StatusCode = 500,
                    Description = "Invalid email format"
                };
            }
            await _writeRepository.AddAsync(new() { Email = email});
            await _writeRepository.SaveAsync();
            return new ApiResponse()
            {
                StatusCode = 204,
            };

        }

        public async Task<ApiResponse> GetAllAsync(int count, int page)
        {
            var response = await _readRepository.GetAll(x=>!x.IsDeleted, count, page, false).ToListAsync();
            return new ApiResponse()
            {
                items = response,
                StatusCode = 200
            };

        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            await _writeRepository.Remove(id);
            await _writeRepository.SaveAsync();
            return new ApiResponse()
            {
                StatusCode = 204
            };
        }
    }
}
