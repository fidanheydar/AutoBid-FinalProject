using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class BlogTag : BaseEntity
    {
        [Key]
        [ForeignKey(nameof(Blog))]
        public Guid BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        [Key]
        [ForeignKey(nameof(Tag))]
        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
