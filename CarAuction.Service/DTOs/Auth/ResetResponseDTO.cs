using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Auth
{
    public record ResetResponseDTO
    {
        public string UserId { get; set; } = null!;
        public string ResetToken { get; set; } = null!;
    }
}
