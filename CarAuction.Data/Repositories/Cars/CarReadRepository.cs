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
    public class CarReadRepository : ReadRepository<Car>, ICarReadRepository
    {
        public CarReadRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
