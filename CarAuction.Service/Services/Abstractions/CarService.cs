using AutoMapper;
using CarAuction.Core.Repositories.CarImages;
using CarAuction.Core.Repositories.Cars;
using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> GetAllAsync(int count, int page)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> SetMainImage(string carId, string imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> UpdateAsync(string id, CarUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
