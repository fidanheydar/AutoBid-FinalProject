using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey(nameof(Admin))]
        public string AdminId { get; set; }
        public virtual AppUser? Admin { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<BlogTag> BlogTags { get; set; }  = new List<BlogTag>();
        public virtual ICollection<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
