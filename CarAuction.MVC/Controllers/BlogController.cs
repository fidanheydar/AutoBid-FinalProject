using AutoMapper;
using CarAuction.Service.DTOs.Blogs;
using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace CarAuction.MVC.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _service;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService service, ICategoryService categoryService, ITagService tagService, IMapper mapper)
        {
            _service = service;
            _categoryService = categoryService;
            _tagService = tagService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _service.GetAllAsync(0,0);
            int TotalCount = ((IEnumerable<BlogGetDto>)result.items).Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)TotalCount / 4);
            ViewBag.CurrentPage = page;
            int count = 4;
            result = await _service.GetAllAsync(count,page);
            return View(result.items);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = await _categoryService.GetAllAsync(0, 0);
            ViewBag.Categories =(IEnumerable<CategoryGetDto>)result.items;
            result = await _tagService.GetAllAsync(0, 0);
            ViewBag.Tags =  (IEnumerable<TagGetDto>)result.items;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostDto dto)
        {
            var result = await _categoryService.GetAllAsync(0, 0);
            ViewBag.Categories = (IEnumerable<CategoryGetDto>)result.items;
            result = await _tagService.GetAllAsync(0, 0);
            ViewBag.Tags = (IEnumerable<TagGetDto>)result.items;
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
             result = await _service.CreateAsync(dto);
            if (result.StatusCode == 404)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var result = await _categoryService.GetAllAsync(0, 0);
            ViewBag.Categories = (IEnumerable<CategoryGetDto>)result.items;
            result = await _tagService.GetAllAsync(0, 0);
            ViewBag.Tags = (IEnumerable<TagGetDto>)result.items;

            result = await _service.GetAsync(id);
            if (result.StatusCode == 404)
            {
                return NotFound();
            }
            return View(_mapper.Map<BlogUpdateDto>(result.items));
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(string id,BlogUpdateDto dto)
        {
            var result = await _categoryService.GetAllAsync(0, 0);
            ViewBag.Categories = (IEnumerable<CategoryGetDto>)result.items;
            result = await _tagService.GetAllAsync(0, 0);
            ViewBag.Tags = (IEnumerable<TagGetDto>)result.items;
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
             result = await _service.UpdateAsync(id,dto);
            if (result.StatusCode == 400)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(string id)
        {
            var result = await _categoryService.GetAllAsync(0, 0);
            ViewBag.Categories = (IEnumerable<CategoryGetDto>)result.items;
            result = await _tagService.GetAllAsync(0, 0);
            ViewBag.Tags = (IEnumerable<TagGetDto>)result.items;
            result = await _service.RemoveAsync(id);
            if(result.StatusCode == 404)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
