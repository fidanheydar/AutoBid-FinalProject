using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Categories;
using CarAuction.Core.Repositories.Fuels;
using CarAuction.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories.Fuels
{
    public class FuelWriteRepository : WriteRepository<Fuel>, IFuelWriteRepository
    {
        public FuelWriteRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
