using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface IIdentityService
    {
        public Task<ApiResponse> Register(RegisterDto registerDto, string role = null);
        public Task<ApiResponse> Login(LoginDto loginDto, int accessTokenLifeTime, string role = null);
        public  Task<ApiResponse> GetAllUsers();
        public Task<ApiResponse> UpdateUser(UpdateDto dto);
    }
}
