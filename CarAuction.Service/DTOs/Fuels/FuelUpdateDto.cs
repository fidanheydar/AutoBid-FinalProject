using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Fuels
{
    public record FuelUpdateDto
    {
        public string Name { get; set; }
    }
}
