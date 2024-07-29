using CarAuction.Core.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    [Index(nameof(Level), IsUnique = true)]
    public class Status : BaseEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        [JsonIgnore]
        public virtual ICollection<Car> Cars { get; set; }
    }
}
