using CarAuction.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Comments
{
    public record CommentPostDto
    {
        public string Text { get; set; }
        public Guid BlogId { get; set; }
    }
}
