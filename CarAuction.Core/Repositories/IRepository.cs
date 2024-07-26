using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
    }
}
