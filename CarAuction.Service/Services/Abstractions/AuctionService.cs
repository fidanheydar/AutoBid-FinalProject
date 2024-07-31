using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Bids;
using CarAuction.Core.Repositories.Cars;
using CarAuction.Core.Repositories.Statuss;
using CarAuction.Service.Exceptions;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Abstractions
{
    public class AuctionService : IAuctionService
    {
        private readonly ICarReadRepository _carReadRepository;
        private readonly ICarWriteRepository _carWriteRepository;
        private readonly IBidReadRepository _bidReadRepository;
        private readonly IMailService _mailService;
        private readonly IStatusReadRepository _statusReadRepository;
        public AuctionService(ICarReadRepository carReadRepository, ICarWriteRepository carWriteRepository, IBidReadRepository bidReadRepository, IMailService mailService, IStatusReadRepository statusReadRepository)
        {
            _carReadRepository = carReadRepository;
            _carWriteRepository = carWriteRepository;
            _bidReadRepository = bidReadRepository;
            _mailService = mailService;
            _statusReadRepository = statusReadRepository;
        }

        public async Task<ApiResponse> FinishAuction(string carId)
        {
            Car car = await _carReadRepository.GetByIdAsync(carId, x => !x.IsDeleted, true, "CarAuctionDetail", "Status");
            if (car == null || car.Status.Level != 2)
                throw new ItemNotFoundException();

            Bid? maxBid = await _bidReadRepository.GetAll(x => x.CarId.ToString() == carId, 0, 0).Include(x => x.User).OrderByDescending(x => x.Count).FirstOrDefaultAsync();


            car.CarAuctionDetail.FinishDate = DateTime.Now;
            if (maxBid == null)
            {
                car.CarAuctionDetail.AuctionWinPrice = car.CarAuctionDetail.InitialPrice;
            }
            else
            {
                car.CarAuctionDetail.AuctionWinPrice = maxBid.Count;
                car.CarAuctionDetail.Winner = maxBid.User;

                await _mailService.SendEmailAsync(new DTOs.Mail.MailRequestDTO()
                {
                    Attachments = null,
                    Subject = "Car Auction Result",
                    Body = $"Congratulation.You Win {car.Vin} Car.Please login and check your account",
                    ToEmails = new() { car.CarAuctionDetail.Winner.Email }
                }
                );
            }
            car.Status = await _statusReadRepository.GetAll(x => x.Level == 3 && !x.IsDeleted, 0, 0).FirstOrDefaultAsync();

            _carWriteRepository.Update(car);
            await _carWriteRepository.SaveAsync();
            return new ApiResponse()
            {
                StatusCode = 203
            };
        }
        public async Task<ApiResponse> StartAuction(string carId)
        {
            Car car = await _carReadRepository.GetByIdAsync(carId, x => !x.IsDeleted, true, "CarAuctionDetail", "Status");
            if (car == null || car.Status.Level != 1)
                throw new ItemNotFoundException();

            car.Status = await _statusReadRepository.GetAll(x => x.Level == 2 && !x.IsDeleted, 0, 0).FirstOrDefaultAsync();
            _carWriteRepository.Update(car);
            await _carWriteRepository.SaveAsync();
            return new ApiResponse()
            {
                StatusCode = 203
            };
        }
        public ApiResponse CheckFinishDate()
        {
            var cars = _carReadRepository.GetAll(x => !x.IsDeleted, 0, 0).Include(x => x.Status).Include(x => x.CarAuctionDetail).Where(x => x.Status.Level != 3).ToList();

            foreach (var car in cars)
            {
                if (car.CarAuctionDetail.FinishDate <= DateTime.Now && car.Status.Level == 2)
                {
                    FinishAuction(car.Id.ToString()).Wait();
                }
                else if (car.CarAuctionDetail.AuctionDate <= DateTime.Now && car.Status.Level == 1)
                {
                    StartAuction(car.Id.ToString()).Wait();
                }
            }

            return new()
            {
                StatusCode = 200
            };
        }
    }
}
