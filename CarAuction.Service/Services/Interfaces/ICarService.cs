using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface ICarService : IService<CarPostDto,CarUpdateDto>
    {
        Task<ApiResponse> SetMainImage(string carId, string imageId);
    }
}
