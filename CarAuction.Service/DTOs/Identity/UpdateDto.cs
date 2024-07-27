using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Identity
{
    public record UpdateDto
    {
        public string Name { get; set; } 
        public string Surname { get; set; } 
        public string Email { get; set; } 
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmedPassword { get; set; }
    }
}
