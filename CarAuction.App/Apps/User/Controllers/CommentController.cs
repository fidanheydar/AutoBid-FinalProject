using CarAuction.Service.DTOs.Comments;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
        [HttpPost]
        [ActionName("MakeComment")]
        public async Task<IActionResult> MakeComment([FromForm] CommentPostDto dto)
        {
                var response = await commentService.CreateAsync(dto);
                return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete([FromRoute]string commentId)
        {
            var response = await commentService.RemoveAsync(commentId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
