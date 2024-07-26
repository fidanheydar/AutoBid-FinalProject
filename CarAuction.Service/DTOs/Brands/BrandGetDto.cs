using CarAuction.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Tags
{
    public record BrandGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
