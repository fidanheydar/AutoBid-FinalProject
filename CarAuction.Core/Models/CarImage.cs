using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class CarImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public Guid CarId { get; set; }
        public bool isMain { get; set; }
        public virtual Car Car { get; set; }
    }
}
