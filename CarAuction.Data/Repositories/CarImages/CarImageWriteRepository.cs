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
    public class CarImageWriteRepository : WriteRepository<CarImage>, ICarImageWriteRepository
    {
        public CarImageWriteRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
