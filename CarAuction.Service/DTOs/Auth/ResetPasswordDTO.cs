using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Auth
{
    public record ResetPasswordDTO
    {
        public string Email { get; set; } = null!;
    }
}
