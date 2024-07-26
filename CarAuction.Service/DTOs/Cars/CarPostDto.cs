using CarAuction.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Cars
{
    public record CarPostDto
    {
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
        public DateTime AuctionDate { get; set; }
        public DateTime FinishDate { get; set; }
        public List<IFormFile> Images { get; set; }

    }
}
