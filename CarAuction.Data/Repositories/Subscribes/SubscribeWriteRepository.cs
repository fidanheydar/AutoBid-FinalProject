using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Subscribes;
using CarAuction.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories.Subscribes
{
    public class SubscribeWriteRepository : WriteRepository<Subscribe>, ISubscribeWriteRepository
    {
        public SubscribeWriteRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
