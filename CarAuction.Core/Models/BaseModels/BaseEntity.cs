using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarAuction.Core.Models.BaseModels
{
    public abstract class BaseEntity 
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
