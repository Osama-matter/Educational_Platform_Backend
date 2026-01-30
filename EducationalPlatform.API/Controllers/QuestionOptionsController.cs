using EducationalPlatform.Application.DTOs.QuestionOption;
using EducationalPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EducationalPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionOptionsController : ControllerBase
    {
        private readonly IQuestionOptionService _questionOptionService;

        public QuestionOptionsController(IQuestionOptionService questionOptionService)
        {
            _questionOptionService = questionOptionService;
        }

        [HttpPost(Routes.Routes.QuestionOptions.CreateQuestionOption)]
        [Authorize]
        public async Task<IActionResult> Create(CreateQuestionOptionDto createQuestionOptionDto)
        {
            var questionOptionId = await _questionOptionService.CreateQuestionOptionAsync(createQuestionOptionDto);
            return CreatedAtAction(nameof(GetById), new { questionOptionId }, createQuestionOptionDto);
        }

        [HttpGet(Routes.Routes.QuestionOptions.GetAllQuestionOptions)]
        public async Task<IActionResult> GetAll()
        {
            var questionOptions = await _questionOptionService.GetQuestionOptionsAsync();
            return Ok(questionOptions);
        }

        [HttpGet(Routes.Routes.QuestionOptions.GetQuestionOptionById)]
        public async Task<IActionResult> GetById(Guid questionOptionId)
        {
            var questionOption = await _questionOptionService.GetQuestionOptionByIdAsync(questionOptionId);
            if (questionOption == null)
            {
                return NotFound();
            }
            return Ok(questionOption);
        }
        [HttpGet(Routes.Routes.QuestionOptions.GetQuestionOptionsByQuestionId)]

        public  async Task<IActionResult> GetByQuestionId(Guid questionId)
        {
            var questionOptions = await _questionOptionService.GetQuestionOptionsByQuestionIdAsync(questionId);
            if (questionOptions == null)
            {
                return NotFound();
            }
            return Ok(questionOptions);
        }

        [HttpPut(Routes.Routes.QuestionOptions.UpdateQuestionOption)]
        [Authorize]
        public async Task<IActionResult> Update(Guid questionOptionId, UpdateQuestionOptionDto updateQuestionOptionDto)
        {
            await _questionOptionService.UpdateQuestionOptionAsync(questionOptionId, updateQuestionOptionDto);
            return NoContent();
        }

        [HttpDelete(Routes.Routes.QuestionOptions.DeleteQuestionOption)]
        [Authorize]
        public async Task<IActionResult> Delete(Guid questionOptionId)
        {
            await _questionOptionService.DeleteQuestionOptionAsync(questionOptionId);
            return NoContent();
        }
    }
}
