using EducationalPlatform.Application.DTOs.CourseFile;
using EducationalPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EducationalPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class CourseFilesController : ControllerBase
    {
        private readonly ICourseFileService _courseFileService;

        public CourseFilesController(ICourseFileService courseFileService)
        {
            _courseFileService = courseFileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _courseFileService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _courseFileService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCourseFileRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get User ID from JWT token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            // Assign the userId as InstructorId
            request.UploadedById = Guid.Parse(userId);
            try
            {
                var result = await _courseFileService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateCourseFileRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _courseFileService.UpdateAsync(id, request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _courseFileService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
