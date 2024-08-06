using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Comments;
using CarAuction.Core.Repositories.Cars;
using CarAuction.Service.DTOs.Comments;
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
    public class CommentService : ICommentService
    {
        private readonly ICommentReadRepository _readRepository;
        private readonly ICommentWriteRepository _writeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IIdentityService _identityService;

        public CommentService(ICommentWriteRepository writeRepository, ICommentReadRepository readRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IIdentityService identityService)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _identityService = identityService;
        }

        public async Task<ApiResponse> CreateAsync(CommentPostDto dto)
        {
            string? userName = _contextAccessor.HttpContext?.User.Identity.Name;
            var user = await _identityService.GetUserByName(userName);

            if (user == null)
                throw new Exception("User not found");


            Comment Comment = _mapper.Map<Comment>(dto);
            Comment.UserId = user.Id;

            await _writeRepository.AddAsync(Comment);
            await _writeRepository.SaveAsync();
            return new ApiResponse()
            {
                items = Comment,
                StatusCode = 201,
            };

        }

        public async Task<ApiResponse> GetAllAsync(int count, int page, Expression<Func<Comment, bool>> expression = null)
        {
            var response =  _readRepository.GetAll(x => !x.IsDeleted, count, page, false);

            if(expression != null)
            {
                response = response.Where(expression);
            }

            return new ApiResponse()
            {
                items = _mapper.Map<List<CommentGetDto>>(await response.Include(x => x.User).ToListAsync()),
                StatusCode = 200
            };

        }

        public async Task<ApiResponse> GetAllByBlog(string blogId)
        {
            var response = await _readRepository.GetAll(x => !x.IsDeleted && x.BlogId.ToString() == blogId, 0, 0, false).Include(x=>x.User).OrderByDescending(x=>x.CreatedAt).ToListAsync();

            var dtos = _mapper.Map<List<CommentGetDto>>(response);

            return new ApiResponse()
            {
                items = dtos,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> RemoveAsync(string id)
        {
            var entity = await _readRepository.GetByIdAsync(id, x => !x.IsDeleted);
            string? userName = _contextAccessor.HttpContext?.User.Identity.Name;

            if (entity == null)
            {
                return new ApiResponse()
                {
                    StatusCode = 404
                };
            }

            var user = await _identityService.GetUserByName(userName);
            if (user == null)
                throw new Exception("User not found");

            if (entity.UserId != user.Id && _contextAccessor.HttpContext.User.IsInRole("User"))
            {
                return new ApiResponse()
                {
                    items = "You cannot remove others` comments",
                    StatusCode = 400
                };
            }


            _writeRepository.Remove(entity);
            await _writeRepository.SaveAsync();
            return new ApiResponse()
            {
                StatusCode = 204
            };
        }
    }
}
