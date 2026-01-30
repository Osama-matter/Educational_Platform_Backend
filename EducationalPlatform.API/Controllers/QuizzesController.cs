using EducationalPlatform.Application.DTOs.Quiz;
using EducationalPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EducationalPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost(Routes.Routes.Quizzes.CreateQuiz)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateQuizDto createQuizDto)
        {
            var quizId = await _quizService.CreateQuizAsync(createQuizDto);
            return Ok(quizId);
        }

        [HttpGet(Routes.Routes.Quizzes.GetAllQuizzes)]
        public async Task<IActionResult> GetAll()
        {
            var quizzes = await _quizService.GetQuizzesAsync();
            return Ok(quizzes);
        }

        [HttpGet(Routes.Routes.Quizzes.GetQuizById)]
        public async Task<IActionResult> GetById(Guid quizId)
        {
            var quiz = await _quizService.GetQuizByIdAsync(quizId);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }

        [HttpGet("admin/{quizId}")]
        public async Task<IActionResult> GetQuizDetailsForAdmin(Guid quizId)
        {
            var quiz = await _quizService.GetQuizDetailsForAdminAsync(quizId);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }

        [HttpPut(Routes.Routes.Quizzes.UpdateQuiz)]
        [Authorize]
        public async Task<IActionResult> Update(Guid quizId, UpdateQuizDto updateQuizDto)
        {
            await _quizService.UpdateQuizAsync(quizId, updateQuizDto);
            return Ok("Updated  Successfully  ");
        }

        [HttpDelete(Routes.Routes.Quizzes.DeleteQuiz)]
        [Authorize]
        public async Task<IActionResult> Delete(Guid quizId)
        {
            await _quizService.DeleteQuizAsync(quizId);
            return NoContent();
        }
    }
}
