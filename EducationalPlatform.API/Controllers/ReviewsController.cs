using EducationalPlatform.API.Routes;
using EducationalPlatform.Application.DTOs.Review;
using EducationalPlatform.Application.Interfaces.Services;
using EducationalPlatform.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EducationalPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet( Routes.Routes.Reviews.GetReviewById)]
        public async Task<IActionResult> GetReviewById(Guid reviewId)
        {
            var review = await _reviewService.GetReviewByIdAsync(reviewId);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpGet(Routes.Routes.Reviews.GetReviewsForCourse)]
        public async Task<IActionResult> GetReviewsForCourse(Guid courseId)
        {
            var reviews = await _reviewService.GetReviewsForCourseAsync(courseId);
            return Ok(reviews);
        }

        [HttpPost(Routes.Routes.Reviews.CreateReview)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto createReviewDto)
        {
            // Get User ID from JWT token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            // Assign the userId as InstructorId
            createReviewDto.UserId = Guid.Parse(userId);


            var review = await _reviewService.CreateReviewAsync(createReviewDto);
            return Ok(review);
        }

        [HttpPut(Routes.Routes.Reviews.UpdateReview)]
        public async Task<IActionResult> UpdateReview(Guid reviewId, [FromBody] UpdateReviewDto updateReviewDto)
        {
            await _reviewService.UpdateReviewAsync(reviewId, updateReviewDto);
            return Ok("Updated  ");
        }

        [HttpPost("{reviewId}/reply")]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> ReplyToReview(Guid reviewId, [FromBody] InstructorReplyDto replyDto)
        {
            await _reviewService.ReplyToReviewAsync(reviewId, replyDto);
            return Ok("Reply added successfully");
        }

        [HttpDelete(Routes.Routes.Reviews.DeleteReview)]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            await _reviewService.DeleteReviewAsync(reviewId);
            return NoContent();
        }
    }
}
