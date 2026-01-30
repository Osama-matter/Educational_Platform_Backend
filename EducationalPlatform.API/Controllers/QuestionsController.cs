using EducationalPlatform.Application.DTOs.Question;
using EducationalPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EducationalPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost(Routes.Routes.Questions.CreateQuestion)]
        [Authorize]
        public async Task<IActionResult> Create(CreateQuestionDto createQuestionDto)
        {
            var questionId = await _questionService.CreateQuestionAsync(createQuestionDto);
            return Ok(questionId);
        }

        [HttpGet(Routes.Routes.Questions.GetAllQuestions)]
        public async Task<IActionResult> GetAll()
        {
            var questions = await _questionService.GetQuestionsAsync();
            return Ok(questions);
        }
        [HttpGet(Routes.Routes.Questions.GetQuestionsByQuizId)]

        public async Task<IActionResult> GetByQuizId(Guid quizId)
        {
            var questions = await _questionService.GetQuestionByQuizeIdAsync(quizId);
            if(questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }

        [HttpGet(Routes.Routes.Questions.GetQuestionById)]
        public async Task<IActionResult> GetById(Guid questionId)
        {
            var question = await _questionService.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPut(Routes.Routes.Questions.UpdateQuestion)]
        [Authorize]
        public async Task<IActionResult> Update(Guid questionId, UpdateQuestionDto updateQuestionDto)
        {
            await _questionService.UpdateQuestionAsync(questionId, updateQuestionDto);
            return NoContent();
        }

        [HttpDelete(Routes.Routes.Questions.DeleteQuestion)]
        [Authorize]
        public async Task<IActionResult> Delete(Guid questionId)
        {
            await _questionService.DeleteQuestionAsync(questionId);
            return NoContent();
        }
    }
}
