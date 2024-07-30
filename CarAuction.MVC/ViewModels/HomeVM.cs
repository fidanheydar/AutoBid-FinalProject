using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Bids;
using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.DTOs.Statuses;
using System.Security.Cryptography;

namespace CarAuction.MVC.ViewModels
{
	public class HomeVM
	{
		public IEnumerable<UserGetDto> Users { get; set; }
		public IEnumerable<CarGetDto> Cars { get; set; }
		public IEnumerable<BidGetDto> Bids { get; set; }
		public IEnumerable<UserGetDto> Admins { get; set; }
		public IEnumerable<StatusGetDto> Statuses { get; set; }
	}
}
