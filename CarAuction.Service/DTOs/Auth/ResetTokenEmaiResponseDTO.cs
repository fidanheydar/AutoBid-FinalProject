using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Auth
{
    public record ResetTokenEmailResponseDTO
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}
