using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Identity
{
    public record LoginDto
    {
        public string EmailorUserName { get; set; } 
        public string Password { get; set; }
    }
}
