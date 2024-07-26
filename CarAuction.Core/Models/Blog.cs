using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string BaseImageUrl { get; set; }
        public string SectionImageUrl { get; set; }
        public string Fact {  get; set; }   
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
    }
}
