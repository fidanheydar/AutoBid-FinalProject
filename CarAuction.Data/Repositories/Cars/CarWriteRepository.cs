using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Cars;
using CarAuction.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories.Cars
{
    public class CarWriteRepository : WriteRepository<Car>, ICarWriteRepository
    {
        public CarWriteRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
