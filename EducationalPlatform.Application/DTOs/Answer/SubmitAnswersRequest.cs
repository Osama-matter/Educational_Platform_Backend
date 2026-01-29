using System.Collections.Generic;

namespace EducationalPlatform.Application.DTOs.Answer
{
    public class SubmitAnswersRequest
    {
        public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
    }
}
