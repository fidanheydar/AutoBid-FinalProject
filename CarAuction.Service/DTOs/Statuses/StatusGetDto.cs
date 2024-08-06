using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Statuses
{
    public record StatusGetDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Level { get; set; }
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
