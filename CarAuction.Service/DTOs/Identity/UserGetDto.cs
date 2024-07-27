using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Identity
{
    public record UserGetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
