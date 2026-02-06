using EducationalPlatform.Application.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task<ReviewDto> GetReviewByIdAsync(Guid id);
        Task<IEnumerable<ReviewDto>> GetReviewsForCourseAsync(Guid courseId);
        Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto);
        Task UpdateReviewAsync(Guid id, UpdateReviewDto updateReviewDto);
        Task ReplyToReviewAsync(Guid id, InstructorReplyDto replyDto);
        Task DeleteReviewAsync(Guid id);
    }
}
