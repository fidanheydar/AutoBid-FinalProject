using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Comments
{
    public record CommentGetDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public Guid BlogId { get; set; }
        public string UserName { get; set; }
        public string CreatedAt { get; set; }
    }
}
