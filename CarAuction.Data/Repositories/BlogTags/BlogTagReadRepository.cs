using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.BlogTags;
using CarAuction.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories.BlogTags
{
    public class BlogTagReadRepository : ReadRepository<BlogTag>, IBlogTagReadRepository
    {
        public BlogTagReadRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
