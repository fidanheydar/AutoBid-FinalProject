using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.Responses;
using CarAuction.Service.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface ICarService : IService<CarPostDto,CarUpdateDto>
    {
        Task<ApiResponse> SetMainImage(string carId, string imageId);
        Task<ApiResponse> RemoveImage(string imageId);
        Task<ApiResponse> AdvancedSearch(AdvancedSearch search);
        public Task<ApiResponse> GetAllImages(string carId);
        public Task<ApiResponse> GetImage(Expression<Func<CarImage, bool>> expression);
        public Task<ApiResponse> SaveImage();
	}
}
