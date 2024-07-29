using CarAuction.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Bids
{
    public record BidGetDto
    {
        public string Id { get; set; }
        public double Count { get; set; }
        public string CarId { get; set; }
        public string UserId { get; set; }
    }
}
