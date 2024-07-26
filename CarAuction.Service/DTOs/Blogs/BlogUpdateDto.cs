using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.DTOs.Blogs
{
    public record BlogUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? BaseImage { get; set; }
        public IFormFile? SectionImage { get; set; }
        public string Fact { get; set; }
        public Guid CategoryId { get; set; }
        public Guid[] TagIds { get; set; }
    }
}
