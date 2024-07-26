using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Fuels
{
    public record FuelGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
