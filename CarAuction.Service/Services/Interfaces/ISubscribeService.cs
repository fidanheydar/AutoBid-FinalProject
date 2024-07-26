using CarAuction.Core.Models;
using CarAuction.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface ISubscribeService
    {
        public Task<ApiResponse> CreateAsync(Subscribe entity);
        public Task<ApiResponse> GetAllAsync(int count, int page);
        public Task<ApiResponse> RemoveAsync(string id);

    }
}
