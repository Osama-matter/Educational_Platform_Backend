using EducationalPlatform.Application.DTOs.Progress;
using EducationalPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EducationalPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;
        public ProgressController(IProgressService progressService)
        {
            _progressService = progressService;
        }

        [HttpGet(Routes.Routes.Progress.GetAllProgress)]
        public async Task<IActionResult> GetAllProgress()
        {
            var result = await _progressService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet(Routes.Routes.Progress.GetProgressById)]
        public async Task<IActionResult> GetProgressById(Guid progressId)
        {
            var result = await _progressService.GetByIdAsync(progressId);
            return Ok(result);
        }

        [HttpPost(Routes.Routes.Progress.CreateProgress)]
        public async Task<IActionResult> CreateProgress([FromBody] CreateLessonProgressDto createLessonProgressDto)
        {
            try
            {
                var result = await _progressService.CreateAsync(createLessonProgressDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = ex.Message ?? string.Empty;
                if (message.Contains("duplicate", StringComparison.OrdinalIgnoreCase) ||
                    message.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase))
                {
                    return Conflict("Lesson progress already exists.");
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating progress.");
            }
        }

        [HttpDelete(Routes.Routes.Progress.DeleteProgress)]
        public IActionResult DeleteProgress(Guid progressId)
        {
            _progressService.DeleteAsync(progressId);
            return NoContent();
        }








    }
}
