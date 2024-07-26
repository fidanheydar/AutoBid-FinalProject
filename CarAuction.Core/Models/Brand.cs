﻿using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Model> Models { get; set; }
    }
}
