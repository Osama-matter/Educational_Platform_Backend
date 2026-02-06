using EducationalPlatform.Application.DTOs.Forum;
using EducationalPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EducationalPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumThreadsController : ControllerBase
    {
        private readonly IForumThreadService _threadService;
        private readonly IForumPostService _postService;

        public ForumThreadsController(IForumThreadService threadService, IForumPostService postService)
        {
            _threadService = threadService;
            _postService = postService;
        }

        [HttpGet(Routes.Routes.ForumThreads.GetAllThreads)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _threadService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet(Routes.Routes.ForumThreads.GetThreadById)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _threadService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost(Routes.Routes.ForumThreads.CreateThread)]
        [Authorize]
        public async Task<IActionResult> Create(CreateForumThreadDto createDto)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            var result = await _threadService.CreateAsync(createDto, userId);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut(Routes.Routes.ForumThreads.UpdateThread)]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, UpdateForumThreadDto updateDto)
        {
            try
            {
                var result = await _threadService.UpdateAsync(id, updateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete(Routes.Routes.ForumThreads.DeleteThread)]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _threadService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet(Routes.Routes.ForumThreads.GetThreadPosts)]
        public async Task<IActionResult> GetPosts(Guid id)
        {
            var result = await _postService.GetByThreadIdAsync(id);
            return Ok(result);
        }
    }
}
