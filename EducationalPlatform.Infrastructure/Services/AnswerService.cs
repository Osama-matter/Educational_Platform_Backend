using EducationalPlatform.Application.DTOs.Answer;
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
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuizAttemptRepository _quizAttemptRepository;
        private readonly IQuestionRepository _questionRepository;

        public AnswerService(IAnswerRepository answerRepository, IQuizAttemptRepository quizAttemptRepository, IQuestionRepository questionRepository)
        {
            _answerRepository = answerRepository;
            _quizAttemptRepository = quizAttemptRepository;
            _questionRepository = questionRepository;
        }

        public async Task SubmitAnswersAsync(Guid quizAttemptId, SubmitAnswersRequest request)
        {
            var quizAttempt = await _quizAttemptRepository.GetByIdAsync(quizAttemptId);
            if (quizAttempt == null || quizAttempt.Status != QuizAttemptStatus.InProgress)
            {
                throw new InvalidOperationException("Quiz attempt is not valid or has already been submitted.");
            }

            var answers = new List<Answer>();
            int totalScore = 0;

            var questionIds = request.Answers.Select(a => a.QuestionId).ToList();  // get question IDs from answers
            var questions = (await _questionRepository.GetAllAsync()).Where(q => questionIds.Contains(q.Id)).ToList(); // to get questions by IDs

            foreach (var answerDto in request.Answers)  // iterate through submitted answers
            {
                var question = questions.FirstOrDefault(q => q.Id == answerDto.QuestionId);  // find the corresponding question
                if (question != null)  // if question exists
                {
                    var correctAnswer = question.Options.FirstOrDefault(o => o.IsCorrect);   // find the correct option
                    if (correctAnswer != null && correctAnswer.Id == answerDto.SelectedOptionId)
                    {
                        totalScore += question.Score;
                    }
                }

                answers.Add(new Answer
                {
                    QuizAttemptId = quizAttemptId,
                    QuestionId = answerDto.QuestionId,
                    SelectedOptionId = answerDto.SelectedOptionId
                });
            }

            await _answerRepository.AddRangeAsync(answers);

            quizAttempt.TotalScore = totalScore;
            quizAttempt.SubmittedAt = DateTime.UtcNow;
            quizAttempt.Status = QuizAttemptStatus.Graded;

            await _quizAttemptRepository.UpdateAsync(quizAttempt);
        }
    }
}
