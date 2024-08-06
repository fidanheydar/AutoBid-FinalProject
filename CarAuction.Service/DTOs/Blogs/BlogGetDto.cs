using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Comments;
using CarAuction.Service.DTOs.Tags;
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
        public CategoryGetDto Category { get; set; }
        public List<TagGetDto> Tags { get; set; }
        public List<CommentGetDto> Comments { get; set; }
    }
}
