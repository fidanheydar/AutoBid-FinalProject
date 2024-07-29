using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Statuses
{
    public record StatusPostDto 
    {
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
