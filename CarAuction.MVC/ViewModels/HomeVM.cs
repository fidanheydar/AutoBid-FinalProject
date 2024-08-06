using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Bids;
using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.DTOs.Statuses;
using CarAuction.Service.Responses;
using System.Security.Cryptography;

namespace CarAuction.MVC.ViewModels
{
	public class HomeVM
    {
        public IEnumerable<UserGetDto> Users { get; set; } = new List<UserGetDto>();
		public IEnumerable<CarGetDto> Cars { get; set; } = new List<CarGetDto>();
		public IEnumerable<BidGetDto> Bids { get; set; } = new List<BidGetDto>();
        public IEnumerable<UserGetDto> Admins { get; set; } = new List<UserGetDto>();
		public IEnumerable<StatusGetDto> Statuses { get; set; } = new List<StatusGetDto>();
        public IEnumerable<ChartResponse> ChartData { get; set; } = new List<ChartResponse>();
    }
}
