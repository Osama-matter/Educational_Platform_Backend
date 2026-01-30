using EducationalPlatform.Application.DTOs.Question;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IQuestionService
    {
        Task<QuestionDto> GetQuestionByIdAsync(Guid id);
        Task<IEnumerable<QuestionDto>> GetQuestionByQuizeIdAsync(Guid id);
        Task<IEnumerable<QuestionDto>> GetQuestionsAsync();
        Task<Guid> CreateQuestionAsync(CreateQuestionDto createQuestionDto);
        Task UpdateQuestionAsync(Guid id, UpdateQuestionDto updateQuestionDto);
        Task DeleteQuestionAsync(Guid id);
    }
}
