using EducationalPlatform.Application.DTOs.QuestionOption;
using EducationalPlatform.Domain.Enums;
using System;
using System.Collections.Generic;

namespace EducationalPlatform.Application.DTOs.Question
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Text
        {
            get => Content;
            set => Content = value;
        }
        public QuestionType QuestionType { get; set; }
        public int Score { get; set; }
        public Guid QuizId { get; set; }
        public List<QuestionOptionDto> Options { get; set; } = new();
    }
}
