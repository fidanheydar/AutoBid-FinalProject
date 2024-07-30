using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories;
using CarAuction.Core.Repositories.Bids;
using CarAuction.Core.Repositories.CarImages;
using CarAuction.Core.Repositories.Cars;
using CarAuction.Core.Repositories.Statuss;
using CarAuction.Data.Repositories;
using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.DTOs.Colors;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.Exceptions;
using CarAuction.Service.Extensions;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using CarAuction.Service.Structs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Miles.Service.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Abstractions
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;
        private readonly ICarReadRepository _carReadRepository;
        private readonly ICarWriteRepository _carWriteRepository;
        private readonly ICarImageReadRepository _carImageReadRepository;
        private readonly ICarImageWriteRepository _carImageWriteRepository;
        private readonly IIdentityService _identityService;
        private readonly IStatusReadRepository _statusReadRepository;
        private readonly IBidReadRepository _bidReadRepository;
        private readonly IMailService _mailService;
        public CarService(ICarImageReadRepository carImageReadRepository, ICarWriteRepository carWriteRepository, ICarReadRepository carReadRepository, IHttpContextAccessor http, IWebHostEnvironment evn, IMapper mapper, ICarImageWriteRepository carImageWriteRepository, IIdentityService identityService, IStatusReadRepository statusReadRepository, IBidReadRepository bidReadRepository, IMailService mailService)
        {
            _carImageReadRepository = carImageReadRepository;
            _carWriteRepository = carWriteRepository;
            _carReadRepository = carReadRepository;
            _http = http;
            _evn = evn;
            _mapper = mapper;
            _carImageWriteRepository = carImageWriteRepository;
            _identityService = identityService;
            _statusReadRepository = statusReadRepository;
            _bidReadRepository = bidReadRepository;
            _mailService = mailService;
        }
        public async Task<ApiResponse> CreateAsync(CarPostDto dto)
        {
            int i = 0;
            Car car = _mapper.Map<Car>(dto);

            car.Status = await _statusReadRepository.GetAll(x => x.Level == 1 && !x.IsDeleted, 0, 0).FirstOrDefaultAsync();


            string userName = _http.HttpContext?.User.Identity.Name;
            var user = await _identityService.GetUserByName(userName);
            if (user == null)
                throw new Exception("User not found");

            car.AdminId = user.Id;

            car.CarAuctionDetail = new()
            {
                AuctionDate = dto.AuctionDate,
                FinishDate = dto.FinishDate,
                InitialPrice = dto.InitialPrice,
            };

            foreach (var item in dto.Images)
            {
                string url = item.CreateImage(_evn.WebRootPath, "Images/Cars");
                CarImage carImage = new()
                {
                    Car = car,
                    ImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                      + $"/Images/Cars/{url}",
                    isMain = i == 0 ? true : false,
                };
                await _carImageWriteRepository.AddAsync(carImage);
                i++;
            }

            await _carWriteRepository.AddAsync(car);
            await _carWriteRepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = car
            };
        }
        public async Task<ApiResponse> GetAllAsync(int count, int page)
        {
            IEnumerable<Car> cars = await _carReadRepository.GetAll(x => !x.IsDeleted, count, page).Include(x => x.Color).Include(x => x.Ban).Include(x => x.Fuel).Include(x => x.CarAuctionDetail)
                .Include(x => x.CarImages).Include(x => x.Status).Include(x => x.Model).ThenInclude(x => x.Brand).Include(x => x.Admin).ToListAsync();

            List<CarGetDto> dtos = _mapper.Map<List<CarGetDto>>(cars);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }
        public async Task<ApiResponse> GetAllAsync(int count, int page, Expression<Func<Car, bool>> expression)
        {
            IEnumerable<Car> cars = await _carReadRepository.GetAll(expression, count, page).Include(x => x.Color).Include(x => x.Ban).Include(x => x.Fuel).Include(x => x.CarAuctionDetail)
                .Include(x => x.CarImages).Include(x => x.Status).Include(x => x.Model).ThenInclude(x => x.Brand).Include(x => x.Admin).ToListAsync();

            List<CarGetDto> dtos = _mapper.Map<List<CarGetDto>>(cars);

            return new ApiResponse
            {
                items = dtos,
                StatusCode = 200
            };
        }
        public async Task<ApiResponse> GetAsync(string id)
        {
            Car car = await _carReadRepository.GetByIdAsync(id, x => !x.IsDeleted, true, "Ban", "Fuel", "CarAuctionDetail", "CarImages", "Model");

            if (car is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            CarGetDto dto = _mapper.Map<CarGetDto>(car);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }
        public async Task<ApiResponse> RemoveAsync(string id)
        {
            Car car = await _carReadRepository.GetByIdAsync(id, x => !x.IsDeleted);
            if (car is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            car.IsDeleted = true;
            await _carWriteRepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = car
            };
        }
        public async Task<ApiResponse> RemoveImage(string imageId)
        {
            CarImage image = await _carImageReadRepository.GetByIdAsync(imageId, x => !x.IsDeleted);
            if (image is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            image.IsDeleted = true;
            await _carWriteRepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = image
            };
        }
        public async Task<ApiResponse> SetMainImage(string carId, string imageId)
        {
            var images = await _carImageReadRepository.GetAll(x => !x.IsDeleted && x.CarId.ToString() == carId, 0, 0).ToListAsync();

            foreach (var image in images)
            {
                if (image.Id.ToString() == imageId)
                    image.isMain = true;
                else
                    image.isMain = false;
            }
            await _carImageWriteRepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 204,
            };
        }
        public async Task<ApiResponse> UpdateAsync(string id, CarUpdateDto dto)
        {
            Car Car = await _carReadRepository.GetByIdAsync(id, x => !x.IsDeleted, true, "Ban", "Fuel", "CarAuctionDetail", "CarImages", "Model");
            if (Car is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }

            string userName = _http.HttpContext?.User.Identity.Name;
            var user = await _identityService.GetUserByName(userName);
            if (user == null)
                throw new Exception("User not found");

            Car.AdminId = user.Id;

            if (dto.Images is not null && dto.Images.Count > 0)
            {
                foreach (var item in dto.Images)
                {
                    var url = item.CreateImage(_evn.WebRootPath, "/Images/Cars");
                    CarImage carImage = new CarImage
                    {
                        Car = Car,
                        ImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                                      + $"/Images/Cars/{url}",
                    };
                    await _carImageWriteRepository.AddAsync(carImage);
                }
            }

            Car = _mapper.Map<CarUpdateDto, Car>(dto, Car);
            Car.CarAuctionDetail = new()
            {
                AuctionDate = dto.AuctionDate,
                FinishDate = dto.FinishDate,
                InitialPrice = dto.InitialPrice,
            };

            await _carWriteRepository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Car
            };

        }
        public async Task<ApiResponse> AdvancedSearch(AdvancedSearch search)
        {
            var cars = _carReadRepository.GetAll(x => !x.IsDeleted, 0, 0);

            if (search.brand is not null)
            {
                cars = cars.Where(x => x.Model.Brand.Name == search.brand);
            }
            if (search.model is not null && search.model != default(Guid))
            {
                cars = cars.Where(x => x.ModelId == search.model);
            }
            if (search.minyear is not null)
            {
                cars = cars.Where(x => x.FabricationYear >= search.minyear);
            }
            if (search.maxyear is not null)
            {
                cars = cars.Where(x => x.FabricationYear <= search.maxyear);
            }
            if (search.minprice is not null)
            {
                cars = cars.Where(x => x.CarAuctionDetail.InitialPrice >= search.minprice);
            }
            if (search.maxprice is not null)
            {
                cars = cars.Where(x => x.CarAuctionDetail.InitialPrice <= search.maxprice);
            }
            if (search.color is not null && search.color != default(Guid))
            {
                cars = cars.Where(x => x.ColorId == search.color);
            }
            if (search.ban is not null && search.ban != default(Guid))
            {
                cars = cars.Where(x => x.BanId == search.ban);
            }
            if (search.fuel is not null && search.fuel != default(Guid))
            {
                cars = cars.Where(x => x.FuelId == search.fuel);
            }
            return new()
            {
                StatusCode = 200,
                items = await cars.ToListAsync(),
            };
        }
        public async Task<ApiResponse> GetAllImages(string carId)
        {
            IEnumerable<CarImage> CarImages = await _carImageReadRepository.GetAll(x => !x.IsDeleted, 0, 0).ToListAsync();

            return new ApiResponse
            {
                items = CarImages,
                StatusCode = 200
            };
        }
        public async Task<ApiResponse> GetImage(Expression<Func<CarImage, bool>> expression)
        {
            CarImage CarImage = await _carImageReadRepository.GetAll(expression, 0, 0).FirstOrDefaultAsync();

            return new ApiResponse
            {
                items = CarImage,
                StatusCode = 200
            };
        }
        public async Task<ApiResponse> SaveImage()
        {
            await _carImageWriteRepository.SaveAsync();
            return new()
            {
                StatusCode = 203
            };
        }
        public async Task<ApiResponse> FinishAuction(string carId)
        {
            Car car = await _carReadRepository.GetByIdAsync(carId, x => !x.IsDeleted, true, "CarAuctionDetail","Status");
            if (car == null || car.Status.Level !=2)
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
            var cars =  _carReadRepository.GetAll(x => !x.IsDeleted, 0, 0).Include(x => x.Status).Include(x=>x.CarAuctionDetail).Where(x=> x.Status.Level != 3).ToList();

            foreach (var car in cars)
            {
                if(car.CarAuctionDetail.FinishDate <= DateTime.Now && car.Status.Level == 2)
                {
                    FinishAuction(car.Id.ToString()).Wait();
                }
                else if(car.CarAuctionDetail.AuctionDate <= DateTime.Now && car.Status.Level == 1)
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
