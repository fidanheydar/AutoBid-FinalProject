using CarAuction.Service.DTOs.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface IBlogService: IService<BlogPostDto, BlogUpdateDto>
    {
    }
}
