using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Bids;
using CarAuction.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface IBidService
    {
        public Task<ApiResponse> CreateAsync(BidPostDto dto);
        public Task<ApiResponse> GetAllAsync(int count, int page, Expression<Func<Bid, bool>> expression = null);
        public Task<ApiResponse> GetAllByCar(string carId);
        public Task<ApiResponse> RemoveAsync(string id);

    }
}
