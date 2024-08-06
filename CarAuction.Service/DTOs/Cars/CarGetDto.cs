using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Bans;
using CarAuction.Service.DTOs.Colors;
using CarAuction.Service.DTOs.Fuels;
using CarAuction.Service.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Cars
{
    public record CarGetDto
    {
        public string Admin { get; set; }
        public Guid Id { get; set; }
        public string Vin { get; set; }
        public int FabricationYear { get; set; }
        public double Odometer { get; set; }
        public int NoGears { get; set; }
        public string Transmission { get; set; }
        public int FuelCity { get; set; }
        public int FuelWay { get; set; }
        public int Power { get; set; }
        public double Motor { get; set; }
        public string Description { get; set; }
        public Guid BanId { get; set; }
        public Guid FuelId { get; set; }
        public Guid ModelId { get; set; }
        public Guid ColorId { get; set; }
		public double InitialPrice { get; set; }
		public double? AuctionWinPrice { get; set; }
		public DateTime AuctionDate { get; set; }
		public DateTime FinishDate { get; set; }
        public string WinnerName { get; set; }
		public virtual ColorGetDto Color { get; set; }
        public virtual ModelGetDto Model { get; set; }
        public virtual FuelGetDto Fuel { get; set; }
        public virtual BanGetDto Ban { get; set; }
        [JsonIgnore]
        public virtual CarAuctionDetail? CarAuctionDetail { get; set; }
        public virtual ICollection<CarImage> CarImages { get; set; }
    }
}
