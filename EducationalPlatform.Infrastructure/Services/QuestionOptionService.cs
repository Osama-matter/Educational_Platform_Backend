using EducationalPlatform.Application.DTOs.QuestionOption;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Application.Interfaces.Services;
using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class QuestionOptionService : IQuestionOptionService
    {
        private readonly IQuestionOptionRepository _questionOptionRepository;
        private readonly IQuestionRepository _questionRepository;
        

        public QuestionOptionService(IQuestionOptionRepository questionOptionRepository , IQuestionRepository questionRepository)
        {
            _questionOptionRepository = questionOptionRepository;
            _questionRepository = questionRepository;
        }

        public async Task<Guid> CreateQuestionOptionAsync(CreateQuestionOptionDto createQuestionOptionDto)
        {
            var question = await _questionRepository.GetByIdAsync(createQuestionOptionDto.QuestionId);
            if(question == null)
            {
                throw new ArgumentException("Invalid QuestionId");
            }
                var questionOption = new QuestionOption
            {
                Text = createQuestionOptionDto.Text,
                IsCorrect = createQuestionOptionDto.IsCorrect,
                QuestionId = createQuestionOptionDto.QuestionId
            };

            await _questionOptionRepository.AddAsync(questionOption);
            return questionOption.Id;
        }

        public async Task DeleteQuestionOptionAsync(Guid id)
        {
            var questionOption = await _questionOptionRepository.GetByIdAsync(id);
            if (questionOption != null)
            {
                await _questionOptionRepository.DeleteAsync(questionOption);
            }
        }

        public async Task<QuestionOptionDto> GetQuestionOptionByIdAsync(Guid id)
        {
            var questionOption = await _questionOptionRepository.GetByIdAsync(id);
            return questionOption == null ? null : new QuestionOptionDto
            {
                Id = questionOption.Id,
                Text = questionOption.Text,
                IsCorrect = questionOption.IsCorrect,
                QuestionId = questionOption.QuestionId
            };
        }

        public async Task<IEnumerable<QuestionOptionDto>> GetQuestionOptionsAsync()
        {
            var questionOptions = await _questionOptionRepository.GetAllAsync();
            return questionOptions.Select(questionOption => new QuestionOptionDto
            {
                Id = questionOption.Id,
                Text = questionOption.Text,
                IsCorrect = questionOption.IsCorrect,
                QuestionId = questionOption.QuestionId
            });
        }

        public Task<IEnumerable<QuestionOptionDto>> GetQuestionOptionsByQuestionIdAsync(Guid questionId)
        {
            var QuestionOptions = _questionOptionRepository.GetByQuestionIdAsync(questionId);
            if (QuestionOptions == null)
            {
                return Task.FromResult<IEnumerable<QuestionOptionDto>>(null);
            }
            return Task.FromResult(QuestionOptions.Result.Select(questionOption => new QuestionOptionDto
            {
                Id = questionOption.Id,
                Text = questionOption.Text,
                IsCorrect = questionOption.IsCorrect,
                QuestionId = questionOption.QuestionId
            }));
        }

        public async Task UpdateQuestionOptionAsync(Guid id, UpdateQuestionOptionDto updateQuestionOptionDto)
        {
            var questionOption = await _questionOptionRepository.GetByIdAsync(id);
            if (questionOption != null)
            {
                questionOption.Text = updateQuestionOptionDto.Text ?? questionOption.Text;
                questionOption.IsCorrect = updateQuestionOptionDto.IsCorrect ?? questionOption.IsCorrect;

                await _questionOptionRepository.UpdateAsync(questionOption);
            }
        }
    }
}
