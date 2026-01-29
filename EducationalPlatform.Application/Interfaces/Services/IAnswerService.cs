using EducationalPlatform.Application.DTOs.Answer;
using System;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IAnswerService
    {
        Task SubmitAnswersAsync(Guid quizAttemptId, SubmitAnswersRequest request);
    }
}
