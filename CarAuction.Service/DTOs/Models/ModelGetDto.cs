using CarAuction.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Models
{
    public record ModelGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Brand Brand { get; set; }
    }
}
