using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories.Categories
{
    public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
