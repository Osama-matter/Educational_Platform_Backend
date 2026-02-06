using EducationalPlatform.Application.DTOs.Forum;
using EducationalPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EducationalPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ForumSubscriptionsController : ControllerBase
    {
        private readonly IForumSubscriptionService _subscriptionService;

        public ForumSubscriptionsController(IForumSubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet(Routes.Routes.ForumSubscriptions.GetMySubscriptions)]
        public async Task<IActionResult> GetMySubscriptions()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            var result = await _subscriptionService.GetUserSubscriptionsAsync(userId);
            return Ok(result);
        }

        [HttpPost(Routes.Routes.ForumSubscriptions.Subscribe)]
        public async Task<IActionResult> Subscribe(CreateForumSubscriptionDto subscribeDto)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            var result = await _subscriptionService.SubscribeAsync(userId, subscribeDto);
            return Ok(result);
        }

        [HttpDelete(Routes.Routes.ForumSubscriptions.Unsubscribe)]
        public async Task<IActionResult> Unsubscribe(Guid threadId)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            var result = await _subscriptionService.UnsubscribeAsync(userId, threadId);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet(Routes.Routes.ForumSubscriptions.IsSubscribed)]
        public async Task<IActionResult> IsSubscribed(Guid threadId)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            var result = await _subscriptionService.IsSubscribedAsync(userId, threadId);
            return Ok(result);
        }
    }
}
