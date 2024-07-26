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

        public ValuesController(IBrandService brandService, IModelService modelService)
        {
            _brandService = brandService;
            _modelService = modelService;
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
            var response = await _brandService.GetAllAsync(0,0);
            return StatusCode(response.StatusCode, response.items);
        }
        [HttpGet("Get_All_Models")]
        public async Task<IActionResult> Get_All_Models()
        {
            var response = await _modelService.GetAllAsync(0, 0);
            return StatusCode(response.StatusCode, response.items);
        }
    }
}
