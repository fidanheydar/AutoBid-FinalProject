using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public Guid BlogId { get; set; }
        public string UserId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual AppUser User { get; set; }
    }
}
