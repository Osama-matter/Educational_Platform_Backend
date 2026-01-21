using EducationalPlatform.Application.DTOs.Courses;
using EducationalPlatform.Application.Interfaces;
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

        private readonly ICourseService  _courseService;

        public CoursesController(ICourseService  courseService)
        {
            _courseService = courseService;
        }



        [HttpPost(Routes.Routes.Courses.CreateCourse)]
        //[Authorize]
        public async Task<IActionResult> Create(CreateCourseDto coursesRequest)
        {
            // Get User ID from JWT token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            // Assign the userId as InstructorId
            coursesRequest.InstructorId = Guid.Parse(userId);


            var result = await _courseService.CreateAsync(coursesRequest);
            return Ok(result);

        }

        [HttpGet(Routes.Routes.Courses.GetAllCourses)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _courseService.GetAllAsync();
            return Ok(result);

        }

        [HttpGet(Routes.Routes.Courses.GetCourseById)]

        public async Task<IActionResult> GetById([FromRoute] Guid courseId)
        {
            var result = await _courseService.GetByIdAsync(courseId);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPut(Routes.Routes.Courses.UpdateCourse)]

        //[Authorize]
        public async Task<IActionResult> Update(Guid courseId, UpdateCourseDto updateCourseDto)
        {
            var result = await _courseService.UpdateAsync(courseId, updateCourseDto);
            return Ok(result);
        }

        [HttpDelete(Routes.Routes.Courses.DeleteCourse)]
        //[Authorize]
         public async Task<IActionResult> Delete(Guid courseId)
         {  
            if (await _courseService.DeleteAsync(courseId)  == true)
            {
                return NoContent();
            }
            throw new Exception("Error deleting course");

        }
    }


}
