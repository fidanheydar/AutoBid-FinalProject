using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.DTOs.Identity;
using System.Security.Cryptography;

namespace CarAuction.MVC.ViewModels
{
	public class HomeVM
	{
		public IEnumerable<UserGetDto> Users { get; set; }
		public IEnumerable<CarGetDto> Cars { get; set; }
		//public IEnumerable<Bid> Bids { get; set; }
		public IEnumerable<UserGetDto> Admins { get; set; }
	}
}
