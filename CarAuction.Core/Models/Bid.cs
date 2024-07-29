using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class Bid: BaseEntity
    {
        public double Count { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
