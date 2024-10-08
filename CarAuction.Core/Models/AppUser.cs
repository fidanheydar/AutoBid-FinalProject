﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarAuction.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; } 
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
        public virtual List<Blog> Blogs { get; set; }
        [InverseProperty("Admin")]
        [JsonIgnore]
        public virtual List<Car> Cars { get; set; }
        [InverseProperty("User")]
        public virtual List<Bid> Bids { get; set; }
        [InverseProperty("Winner")]
        public virtual List<CarAuctionDetail> CarAuctionDetails { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
