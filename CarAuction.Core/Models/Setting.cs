﻿using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class Setting : BaseEntity
    {
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string LogoUrl { get; set; }
        public string AboutImageUrl { get; set; }
        public string WorkHours {  get; set; }
    }
}
