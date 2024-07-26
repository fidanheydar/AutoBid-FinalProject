using CarAuction.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class Subscribe : BaseEntity
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
