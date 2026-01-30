using EducationalPlatform.Application.DTOs.Question;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Application.Interfaces.Services;
using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizRepository _quizRepository;

        public QuestionService(IQuestionRepository questionRepository, IQuizRepository quizRepository)
        {
            _questionRepository = questionRepository;
            _quizRepository = quizRepository;
        }

        public async Task<Guid> CreateQuestionAsync(CreateQuestionDto createQuestionDto)
        {
            var quiz = await _quizRepository.GetByIdAsync(createQuestionDto.QuizId);
            if (quiz == null)
            {
                throw new ArgumentException("Quiz not found.");
            }
                var question = new Question
            {
                Content = createQuestionDto.Content,
                QuestionType = createQuestionDto.QuestionType,
                Score = createQuestionDto.Score,
                QuizId = createQuestionDto.QuizId
            };

            await _questionRepository.AddAsync(question);
            return question.Id;
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question != null)
            {
                await _questionRepository.DeleteAsync(question);
            }
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(Guid id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            return question == null ? null : new QuestionDto
            {
                Id = question.Id,
                Content = question.Content,
                QuestionType = question.QuestionType,
                Score = question.Score,
                QuizId = question.QuizId
            };
        }

        public Task<IEnumerable<QuestionDto>> GetQuestionByQuizeIdAsync(Guid id)
        {
            var questions = _questionRepository.GetByQuizIdAsync(id);
            if (questions == null)
            {
                return Task.FromResult<IEnumerable<QuestionDto>>(null);
            }
            return questions.ContinueWith(task => task.Result.Select(question => new QuestionDto
            {
                Id = question.Id,
                Content = question.Content,
                QuestionType = question.QuestionType,
                Score = question.Score,
                QuizId = question.QuizId
            }));
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsAsync()
        {
            var questions = await _questionRepository.GetAllAsync();
            return questions.Select(question => new QuestionDto
            {
                Id = question.Id,
                Content = question.Content,
                QuestionType = question.QuestionType,
                Score = question.Score,
                QuizId = question.QuizId
            });
        }

        public async Task UpdateQuestionAsync(Guid id, UpdateQuestionDto updateQuestionDto)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question != null)
            {
                question.Content = updateQuestionDto.Content ?? question.Content;
                question.QuestionType = updateQuestionDto.QuestionType ?? question.QuestionType;
                question.Score = updateQuestionDto.Score ?? question.Score;

                await _questionRepository.UpdateAsync(question);
            }
        }
    }
}
