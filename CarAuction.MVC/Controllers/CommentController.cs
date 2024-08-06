using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.MVC.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CommentController(ICommentService service) : Controller
    {
        private readonly ICommentService _service = service;

        public async Task<IActionResult> Index(string blogId)
        {
            var result = await _service.GetAllByBlog(blogId);
            return View(result.items);
        }
        public async Task<IActionResult> Remove(string id)
        {
            var result = await _service.RemoveAsync(id);
            return RedirectToAction("index", "blog");
        }
    }
}
