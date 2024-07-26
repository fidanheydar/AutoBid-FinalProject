using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Bans;
using CarAuction.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAuction.Core.Repositories.Subscribes;

namespace CarAuction.Data.Repositories.Subscribes
{
    public class SubscribeReadRepository : ReadRepository<Subscribe>, ISubscribeReadRepository
    {
        public SubscribeReadRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
