using EducationalPlatform.Application.DTOs.QuestionOption;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IQuestionOptionService
    {
        Task<QuestionOptionDto> GetQuestionOptionByIdAsync(Guid id);
        Task<IEnumerable<QuestionOptionDto>> GetQuestionOptionsAsync();
        Task<Guid> CreateQuestionOptionAsync(CreateQuestionOptionDto createQuestionOptionDto);
        Task UpdateQuestionOptionAsync(Guid id, UpdateQuestionOptionDto updateQuestionOptionDto);
        Task DeleteQuestionOptionAsync(Guid id);
    }
}
