using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface ITagService
    {
        public Task<ApiResponse> CreateAsync(TagPostDto dto);
        public Task<ApiResponse> GetAsync(string id);
        public Task<ApiResponse> GetAllAsync(int count, int page);
        public Task<ApiResponse> UpdateAsync(string id, TagUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(string id);
    }
}
