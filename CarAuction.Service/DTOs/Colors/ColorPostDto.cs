using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Colors
{
    public record ColorPostDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
