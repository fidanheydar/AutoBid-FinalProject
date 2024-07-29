using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class Model:BaseEntity
    {
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        [JsonIgnore]
        public virtual Brand Brand { get; set; }
        [JsonIgnore]
        public virtual ICollection<Car> Cars { get; set; }
    }
}
