using EducationalPlatform.Application.DTOs.Review;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Application.Interfaces.Services;
using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;

        public ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository, ICourseRepository courseRepository)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }

        public async Task<ReviewDto> GetReviewByIdAsync(Guid id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) return null;

            return new ReviewDto
            {
                Id = review.Id,
                Rate = review.Rate,
                Comment = review.Comment,
                UserId = review.UserId,
                UserName = review.User.UserName,
                InstructorReply = review.InstructorReply,
                CourseId = review.CourseId,
                CreatedAt = review.CreatedAt
            };
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsForCourseAsync(Guid courseId)
        {
            // 1. Fetch reviews AND include the User data in ONE database call
            // Ensure your repository method uses .Include(r => r.User)
            var reviews = await _reviewRepository.GetAllRewviewByCouseIDAsync(courseId);

            // 2. Map to DTO synchronously (No more 'await' inside Select)
            var reviewDtos = reviews.Select(review => new ReviewDto
            {
                Id = review.Id,
                Rate = review.Rate,
                Comment = review.Comment,
                UserId = review.UserId,
                UserName = review.User?.UserName ?? "Anonymous", // Use the loaded User property
                CourseId = review.CourseId,
    
                InstructorReply = review.InstructorReply, // Added for PIC 5 requirements
                CreatedAt = review.CreatedAt
            }).ToList();

            return reviewDtos;
        }

        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            var Course = await _courseRepository.GetByIdAsync(createReviewDto.CourseId);
            if (Course ==  null)
            {
                throw new Exception("Course not found");
            }

            var User = await _userRepository.GetByIdAsync(createReviewDto.UserId);

            if (User == null)
            {
                throw new Exception("User not found");
            }

            var review = new Review(createReviewDto.Comment,
                                    createReviewDto.Rate,
                                    createReviewDto.CourseId,
                                    createReviewDto.UserId);

            await _reviewRepository.AddAsync(review);
     
            return new ReviewDto
            {
                Id = review.Id,
                Rate = review.Rate,
                Comment = review.Comment,
                UserId = review.UserId,
                UserName = User?.UserName,
        
                CourseId = review.CourseId,
                CreatedAt = review.CreatedAt
            };




        }

        public async Task UpdateReviewAsync(Guid id, UpdateReviewDto updateReviewDto)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) return;

            review.Rate = updateReviewDto.Rate;
            review.Comment = updateReviewDto.Comment;

            _reviewRepository.Update(review);
        }

        public async Task ReplyToReviewAsync(Guid id, InstructorReplyDto replyDto)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) throw new Exception("Review not found");

            review.InstructorReply = replyDto.Reply;
            review.RepliedAt = DateTime.UtcNow;

            _reviewRepository.Update(review);
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) return;

            _reviewRepository.Delete(review);
        }
    }
}
