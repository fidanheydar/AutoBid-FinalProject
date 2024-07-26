using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<ApiResponse> CreateAsync(CategoryPostDto dto);
        public Task<ApiResponse> GetAsync(string id);
        public Task<ApiResponse> GetAllAsync(int count, int page);
        public Task<ApiResponse> UpdateAsync(string id, CategoryUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(string id);
    }
}
