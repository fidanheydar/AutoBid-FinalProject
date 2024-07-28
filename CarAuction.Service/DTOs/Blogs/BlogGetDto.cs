using CarAuction.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Blogs
{
    public record BlogGetDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BaseImageUrl { get; set; }
        public string SectionImageUrl { get; set; }
        public string Fact { get; set; }
        public string Author  { get; set; }
        public Category Category { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
