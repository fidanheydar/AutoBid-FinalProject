using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories;
using CarAuction.Core.Repositories.CarImages;
using CarAuction.Core.Repositories.Cars;
using CarAuction.Data.Repositories;
using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.DTOs.Colors;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.Extensions;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        public CarService(ICarImageReadRepository carImageReadRepository, ICarWriteRepository carWriteRepository, ICarReadRepository carReadRepository, IHttpContextAccessor http, IWebHostEnvironment evn, IMapper mapper, ICarImageWriteRepository carImageWriteRepository)
        {
            _carImageReadRepository = carImageReadRepository;
            _carWriteRepository = carWriteRepository;
            _carReadRepository = carReadRepository;
            _http = http;
            _evn = evn;
            _mapper = mapper;
            _carImageWriteRepository = carImageWriteRepository;
        }

        public async Task<ApiResponse> CreateAsync(CarPostDto dto)
        {
            int i = 0;
            Car car = _mapper.Map<Car>(dto);

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
                      + $"Images/Cars/{url}",
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
            IEnumerable<Car> cars = await _carReadRepository.GetAll(x => !x.IsDeleted, count, page).Include(x => x.Ban).Include(x => x.Fuel).Include(x => x.CarAuctionDetail)
                .Include(x => x.CarImages).Include(x => x.Model).ThenInclude(x => x.Brand).ToListAsync();

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

        public async Task<ApiResponse> SetMainImage(string carId, string imageId)
        {
           var images =  await _carImageReadRepository.GetAll(x => !x.IsDeleted && x.CarId.ToString() == carId,0,0).ToListAsync();

            foreach (var image in images)
            {
                if(image.Id.ToString() == imageId)
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

            if (dto.Images is not null && dto.Images.Count > 0)
            {
                foreach (var item in dto.Images)
                {
                    var url = item.CreateImage(_evn.WebRootPath, "Images/Cars");
                    CarImage carImage = new CarImage
                    {
                        Car = Car,
                        ImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                                      + $"Images/Cars/{url}",
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
    }
}
