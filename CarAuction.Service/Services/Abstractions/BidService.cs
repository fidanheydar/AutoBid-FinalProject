using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Bids;
using CarAuction.Core.Repositories.Cars;
using CarAuction.Service.DTOs.Bids;
using CarAuction.Service.Exceptions;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CarAuction.Service.Services.Abstractions
{
    public class BidService : IBidService
    {
        private readonly IBidReadRepository _readRepository;
        private readonly IBidWriteRepository _writeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IIdentityService _identityService;
        private readonly ICarReadRepository _carReadRepository;

        public BidService(IBidWriteRepository writeRepository, IBidReadRepository readRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IIdentityService identityService, ICarReadRepository carReadRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _identityService = identityService;
            _carReadRepository = carReadRepository;
        }

        public async Task<ApiResponse> CreateAsync(BidPostDto dto)
        {
            string? userName = _contextAccessor.HttpContext?.User.Identity.Name;
            var user = await _identityService.GetUserByName(userName);
            if (user == null)
                throw new Exception("User not found");

            var car = await _carReadRepository.GetByIdAsync(dto.CarId.ToString(), x => !x.IsDeleted, true, "Status");
            if(car.Status.Level !=2)
                throw new Exception("Invalid car id for bidding");



            var bids = await _readRepository.GetAll(x => !x.IsDeleted && x.CarId == dto.CarId, 0, 0).OrderByDescending(x => x.Count).ToListAsync();
            double range;
            if (bids.Count >= 2)
            {
                range = bids[0].Count - bids[1].Count;
                if (dto.Count < bids[0].Count + range)
                    throw new InvalidBidException($"You must bid more than {bids[0].Count+range} and range is {range}");
            }
            else
            {
                range = 100;

                if (bids.Count > 0)
                {
                    if (dto.Count < bids[0].Count + range)
                        throw new InvalidBidException($"You must bid more than {bids[0].Count} and range is {range}");
                }
                else
                {
                    var price = (await _carReadRepository.GetByIdAsync(dto.CarId.ToString(), x => !x.IsDeleted, false, "CarAuctionDetail")).CarAuctionDetail.InitialPrice;

                    if (dto.Count < price + range)
                        throw new InvalidBidException($"You must bid more than {price} and range is {range}");
                }
            }




            Bid bid = _mapper.Map<Bid>(dto);
            bid.UserId = user.Id;

            await _writeRepository.AddAsync(bid);
            await _writeRepository.SaveAsync();
            return new ApiResponse()
            {
                StatusCode = 204,
            };

        }

        public async Task<ApiResponse> GetAllAsync(int count, int page, Expression<Func<Bid, bool>> expression = null)
        {
            var response =  _readRepository.GetAll(x => !x.IsDeleted, count, page, false);

            if(expression != null)
            {
                response = response.Where(expression);
            }

            return new ApiResponse()
            {
                items = _mapper.Map<List<BidGetDto>>(await response.Include(x => x.User).ToListAsync()),
                StatusCode = 200
            };

        }

        public async Task<ApiResponse> GetAllByCar(string carId)
        {
            var response = await _readRepository.GetAll(x => !x.IsDeleted && x.CarId.ToString() == carId, 0, 0, false).Include(x=>x.User).OrderByDescending(x=>x.Count).ToListAsync();

            var dtos = _mapper.Map<List<BidGetDto>>(response);

            return new ApiResponse()
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            await _writeRepository.Remove(id);
            await _writeRepository.SaveAsync();
            return new ApiResponse()
            {
                StatusCode = 204
            };
        }
    }
}
