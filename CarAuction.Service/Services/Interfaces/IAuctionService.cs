using CarAuction.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public  interface IAuctionService
    {
        Task<ApiResponse> FinishAuction(string carId);
        Task<ApiResponse> StartAuction(string carId);
        ApiResponse CheckFinishDate();
    }
}
