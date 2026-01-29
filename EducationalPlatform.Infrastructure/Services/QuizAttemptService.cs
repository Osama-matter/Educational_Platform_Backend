using EducationalPlatform.Application.DTOs.QuizAttempt;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Application.Interfaces.Services;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class QuizAttemptService : IQuizAttemptService
    {
        private readonly IQuizAttemptRepository _quizAttemptRepository;
        private readonly IQuizRepository _quizRepository;

        public QuizAttemptService(IQuizAttemptRepository quizAttemptRepository, IQuizRepository quizRepository)
        {
            _quizAttemptRepository = quizAttemptRepository;
            _quizRepository = quizRepository;
        }

        public async Task<Guid> CreateQuizAttemptAsync(CreateQuizAttemptDto createQuizAttemptDto)
        {
            var existingAttempts = await _quizAttemptRepository.GetByUserIdAndQuizIdAsync(createQuizAttemptDto.UserId, createQuizAttemptDto.QuizId);
            if (existingAttempts.Any(a => a.Status == QuizAttemptStatus.Graded))
            {
                throw new InvalidOperationException("You have already completed this quiz.");
            }

            var quiz = await _quizRepository.GetByIdAsync(createQuizAttemptDto.QuizId);
            if (quiz == null)
            {
                throw new ArgumentException("Quiz not found");
            }
            if(quiz.AvailableFrom > DateTime.UtcNow || quiz.AvailableTo < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Quiz is not currently available.");
            }


            var quizAttempt = new QuizAttempt
            {
                UserId = createQuizAttemptDto.UserId,
                QuizId = createQuizAttemptDto.QuizId,
                StartedAt = DateTime.UtcNow,
                Status = QuizAttemptStatus.InProgress,
                TotalScore = 0
            };

            await _quizAttemptRepository.AddAsync(quizAttempt);
            return quizAttempt.Id;
        }

        public async Task DeleteQuizAttemptAsync(Guid id)
        {
            var quizAttempt = await _quizAttemptRepository.GetByIdAsync(id);
            if (quizAttempt != null)
            {
                await _quizAttemptRepository.DeleteAsync(quizAttempt);
            }
        }

        public async Task<QuizAttemptDto> GetQuizAttemptByIdAsync(Guid id)
        {
            var quizAttempt = await _quizAttemptRepository.GetByIdAsync(id);
            return quizAttempt == null ? null : new QuizAttemptDto
            {
                Id = quizAttempt.Id,
                UserId = quizAttempt.UserId,
                QuizId = quizAttempt.QuizId,
                StartedAt = quizAttempt.StartedAt,
                SubmittedAt = quizAttempt.SubmittedAt,
                TotalScore = quizAttempt.TotalScore,
                Status = quizAttempt.Status
            };
        }

        public async Task<IEnumerable<QuizAttemptDto>> GetQuizAttemptsAsync()
        {
            var quizAttempts = await _quizAttemptRepository.GetAllAsync();
            return quizAttempts.Select(quizAttempt => new QuizAttemptDto
            {
                Id = quizAttempt.Id,
                UserId = quizAttempt.UserId,
                QuizId = quizAttempt.QuizId,
                StartedAt = quizAttempt.StartedAt,
                SubmittedAt = quizAttempt.SubmittedAt,
                TotalScore = quizAttempt.TotalScore,
                Status = quizAttempt.Status
            });
        }

        public async Task UpdateQuizAttemptAsync(Guid id, UpdateQuizAttemptDto updateQuizAttemptDto)
        {
            var quizAttempt = await _quizAttemptRepository.GetByIdAsync(id);
            if (quizAttempt != null)
            {
                quizAttempt.TotalScore = updateQuizAttemptDto.TotalScore;
                quizAttempt.Status = updateQuizAttemptDto.Status;
                quizAttempt.SubmittedAt = DateTime.UtcNow;

                await _quizAttemptRepository.UpdateAsync(quizAttempt);
            }
        }
    }
}
