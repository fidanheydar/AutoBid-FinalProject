﻿using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [ActionName("GetCategories")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await _categoryService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [ActionName("GetCategory")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await _categoryService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
