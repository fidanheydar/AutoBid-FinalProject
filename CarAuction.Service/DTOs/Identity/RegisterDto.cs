using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Identity
{
    public record RegisterDto
    {
        public string Name { get; set; } 
        public string Surname { get; set; } 
        public string Email { get; set; } 
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmedPassword { get; set; } 
    }
}
