﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Tags
{
    public record TagGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
