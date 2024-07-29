using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Bids;
using CarAuction.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories.Categories
{
    public class BidReadRepository : ReadRepository<Bid>, IBidReadRepository
    {
        public BidReadRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
