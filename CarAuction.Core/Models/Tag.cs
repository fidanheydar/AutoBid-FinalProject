using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<BlogTag> BlogTags { get; set; }
    }
}
