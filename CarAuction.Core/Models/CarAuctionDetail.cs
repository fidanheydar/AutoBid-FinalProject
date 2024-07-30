using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class CarAuctionDetail :BaseEntity
    {
        public double InitialPrice { get; set; }
        public double AuctionWinPrice { get; set; }
        public DateTime AuctionDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string? WinnerId { get; set; }
        public virtual AppUser Winner { get; set; }
        [JsonIgnore]
        public virtual Car Car { get; set; }
    }
}
