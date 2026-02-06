using EducationalPlatform.Application.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Services.Reviews
{
    public interface IReviewService
    {
        Task<ReviewDto> GetReviewByIdAsync(Guid reviewId);
        Task<List<ReviewDto>> GetReviewsForCourseAsync(Guid courseId);
        Task<ReviewDto> CreateReviewAsync(CreateReviewDto review);
        Task UpdateReviewAsync(Guid reviewId, UpdateReviewDto review);
        Task DeleteReviewAsync(Guid reviewId);
        Task ReplyToReviewAsync(Guid reviewId, InstructorReplyDto reply);
    }
}
