using EducationalPlatform.Application.DTOs.Courses;
using EducationalPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EducationalPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        private readonly ICourseService _courseService;
        private readonly ILessonService _lessonService;

        public CoursesController(ICourseService courseService, ILessonService lessonService)
        {
            _courseService = courseService;
            _lessonService = lessonService;
        }



        [HttpPost(Routes.Routes.Courses.CreateCourse)]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Create([FromForm] CreateCourseDto coursesRequest)
        {
            try
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdStr))
                    return Unauthorized();

                var userId = Guid.Parse(userIdStr);

                // Always set InstructorId from JWT to ensure it exists in AspNetUsers
                coursesRequest.InstructorId = userId;

                var result = await _courseService.CreateAsync(coursesRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, detail = ex.InnerException?.Message });
            }
        }

        [HttpGet(Routes.Routes.Courses.GetAllCourses)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _courseService.GetAllAsync();
            return Ok(result);

        }

        [HttpGet(Routes.Routes.Courses.GetCourseById)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] Guid courseId)
        {
            var result = await _courseService.GetByIdAsync(courseId);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{courseId}/lessons")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLessonsForCourse(Guid courseId)
        {
            var lessons = await _lessonService.GetAllLessonsForCourseAsync(courseId);
            return Ok(lessons);
        }


        [HttpPut(Routes.Routes.Courses.UpdateCourse)]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Update(Guid courseId, [FromForm] UpdateCourseDto updateCourseDto)
        {
            try
            {
                var result = await _courseService.UpdateAsync(courseId, updateCourseDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, detail = ex.InnerException?.Message });
            }
        }

        [HttpDelete(Routes.Routes.Courses.DeleteCourse)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid courseId)
        {  
            if (await _courseService.DeleteAsync(courseId))
            {
                return NoContent();
            }
            throw new Exception("Error deleting course");
        }
    }


}
