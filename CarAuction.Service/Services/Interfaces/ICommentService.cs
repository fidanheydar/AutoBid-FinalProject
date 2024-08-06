using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Comments;
using CarAuction.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<ApiResponse> CreateAsync(CommentPostDto dto);
        public Task<ApiResponse> GetAllAsync(int count, int page, Expression<Func<Comment, bool>> expression = null);
        public Task<ApiResponse> GetAllByBlog(string blogId);
        public Task<ApiResponse> RemoveAsync(string id);

    }
}
