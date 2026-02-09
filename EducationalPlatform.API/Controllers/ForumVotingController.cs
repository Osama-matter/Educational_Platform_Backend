using EducationalPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EducationalPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ForumVotingController : ControllerBase
    {
        private readonly IForumVotingService _votingService;

        public ForumVotingController(IForumVotingService votingService)
        {
            _votingService = votingService;
        }

        [HttpPost("{postId}/vote")]
        public async Task<IActionResult> Vote(Guid postId, [FromQuery] int value)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            try
            {
                var newVoteCount = await _votingService.VoteAsync(postId, userId, value);
                return Ok(new { voteCount = newVoteCount });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("thread/{threadId}/vote")]
        public async Task<IActionResult> VoteThread(Guid threadId, [FromQuery] int value)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            try
            {
                var newVoteCount = await _votingService.VoteThreadAsync(threadId, userId, value);
                return Ok(new { voteCount = newVoteCount });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{postId}/count")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVoteCount(Guid postId)
        {
            var count = await _votingService.GetVoteCountAsync(postId);
            return Ok(new { voteCount = count });
        }

        [HttpGet("thread/{threadId}/count")]
        [AllowAnonymous]
        public async Task<IActionResult> GetThreadVoteCount(Guid threadId)
        {
            var count = await _votingService.GetThreadVoteCountAsync(threadId);
            return Ok(new { voteCount = count });
        }

        [HttpGet("{postId}/my-vote")]
        public async Task<IActionResult> GetMyVote(Guid postId)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            var value = await _votingService.GetUserVoteAsync(postId, userId);
            return Ok(new { value = value });
        }

        [HttpGet("thread/{threadId}/my-vote")]
        public async Task<IActionResult> GetMyThreadVote(Guid threadId)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            var value = await _votingService.GetUserThreadVoteAsync(threadId, userId);
            return Ok(new { value = value });
        }
    }
}
