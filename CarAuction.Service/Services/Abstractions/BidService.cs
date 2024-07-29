using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Bids;
using CarAuction.Service.DTOs.Bids;
using CarAuction.Service.Exceptions;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public BidService(IBidWriteRepository writeRepository, IBidReadRepository readRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IIdentityService identityService)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _identityService = identityService;
        }

        public async Task<ApiResponse> CreateAsync(BidPostDto dto)
        {
            string? userName = _contextAccessor.HttpContext?.User.Identity.Name;
            var user = await _identityService.GetUserByName(userName);
            if (user == null)
                throw new Exception("User not found");

            if ((await _readRepository.GetAll(x => !x.IsDeleted && x.CarId == dto.CarId, 0, 0).OrderByDescending(x => x.Count).FirstOrDefaultAsync())?.Count > dto.Count)
                throw new InvalidBidException();

            Bid bid = _mapper.Map<Bid>(dto);
            bid.UserId = user.Id;

            await _writeRepository.AddAsync(bid);
            await _writeRepository.SaveAsync();
            return new ApiResponse()
            {
                StatusCode = 204,
            };

        }

        public async Task<ApiResponse> GetAllAsync(int count, int page)
        {
            var response = await _readRepository.GetAll(x => !x.IsDeleted, count, page, false).ToListAsync();
            return new ApiResponse()
            {
                items = response,
                StatusCode = 200
            };

        }

        public async Task<ApiResponse> GetAllByCar(string carId)
        {
            var response = await _readRepository.GetAll(x => !x.IsDeleted && x.CarId.ToString() == carId, 0, 0, false).ToListAsync();
            return new ApiResponse()
            {
                items = response,
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
