using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<TokenResponseDTO> CreateAccessTokenAsync(AppUser user, int minute);
        public string CreateRefreshToken();
    }
}
