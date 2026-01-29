using EducationalPlatform.Domain.Enums;

namespace EducationalPlatform.Application.DTOs.Question
{
    public class UpdateQuestionDto
    {
        public string Content { get; set; }
        public QuestionType? QuestionType { get; set; }
        public int? Score { get; set; }
    }
}
