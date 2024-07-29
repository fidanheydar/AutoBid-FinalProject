using CarAuction.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Bids
{
    public record BidPostDto
    {
        public double Count { get; set; }
        public Guid CarId { get; set; }
    }
}
