using CarAuction.Service.DTOs.Blogs;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Models;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IModelService _modelService;
        private readonly IBlogService _blogService;
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;

        public ValuesController(IBrandService brandService, IModelService modelService, IBlogService blogService, ITagService tagService, ICategoryService categoryService)
        {
            _brandService = brandService;
            _modelService = modelService;
            _blogService = blogService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        [HttpPost("Create_Brand")]
        public async Task<IActionResult> Create_Brand(BrandPostDto dto)
        {
            var response = await _brandService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response.items);
        }
        [HttpPost("Create_Model")]
        public async Task<IActionResult> Create_Model(ModelPostDto dto)
        {
            var response = await _modelService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response.items);
        }

        [HttpGet("Get_All_Brands")]
        public async Task<IActionResult> Get_All_Brands()
        {
            var response = await _brandService.GetAllAsync(0, 0);
            return StatusCode(response.StatusCode, response.items);
        }
        [HttpGet("Get_All_Models")]
        public async Task<IActionResult> Get_All_Models()
        {
            var response = await _modelService.GetAllAsync(0, 0);
            return StatusCode(response.StatusCode, response.items);
        }

        [HttpPost("Create_Tag")]
        public async Task<IActionResult> Create_Tag(TagPostDto dto)
        {
            var response = await _tagService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response.items);
        }

        [HttpPost("Create_Category")]
        public async Task<IActionResult> Create_Category(CategoryPostDto dto)
        {
            var response = await _categoryService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response.items);
        }
        [HttpPost("Create_Blog")]
        public async Task<IActionResult> Create_Blog([FromForm] BlogPostDto dto)
        {
            var response = await _blogService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response.items);
        }
        [HttpPut("Update_Blog")]
        public async Task<IActionResult> Update_Blog([FromQuery]string id,[FromForm] BlogUpdateDto dto)
        {
            var response = await _blogService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response.items);
        }
        [HttpGet("Get_All_Blog")]
        public async Task<IActionResult> Get_All_Blog()
        {
            var response = await _blogService.GetAllAsync(0,0);
            return StatusCode(response.StatusCode, response.items);
        }
    }
}
