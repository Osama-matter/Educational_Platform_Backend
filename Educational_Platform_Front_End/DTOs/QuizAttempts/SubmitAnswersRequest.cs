using System.Collections.Generic;

namespace Educational_Platform_Front_End.DTOs.QuizAttempts
{
    public class SubmitAnswersRequest
    {
        public List<AnswerDto> Answers { get; set; } = new();
    }
}
