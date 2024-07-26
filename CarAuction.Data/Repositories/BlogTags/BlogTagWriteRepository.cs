using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Blogs;
using CarAuction.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAuction.Core.Repositories.BlogTags;

namespace CarAuction.Data.Repositories.BlogTags
{
    public class BlogTagWriteRepository : WriteRepository<BlogTag>, IBlogTagWriteRepository
    {
        public BlogTagWriteRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
