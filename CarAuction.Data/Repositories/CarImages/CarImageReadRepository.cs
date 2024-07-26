using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.CarImages;
using CarAuction.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories.CarImages
{
    public class CarImageReadRepository : ReadRepository<CarImage>, ICarImageReadRepository
    {
        public CarImageReadRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
