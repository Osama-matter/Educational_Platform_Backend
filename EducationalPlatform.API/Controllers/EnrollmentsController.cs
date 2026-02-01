using EducationalPlatform.API.Routes;
using EducationalPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
    
namespace EducationalPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        // POST: api/enrollments
        [HttpPost(Routes.Routes.Enrollments.CreateEnrollment)]
        public async Task<ActionResult<EducationalPlatform.Application.DTOs.Enrollments.EnrollmentDto>> Create(
            [FromQuery] Guid studentId,
            [FromQuery] Guid courseId)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            // Assign the userId as InstructorId
            studentId = Guid.Parse(userId);
            var result = await _enrollmentService.CreateAsync(studentId, courseId);
            return Ok(result);
        }

        // GET: api/enrollments
        [HttpGet(Routes.Routes.Enrollments.GetAllEnrollments)]
        public async Task<ActionResult<IEnumerable<EducationalPlatform.Application.DTOs.Enrollments.EnrollmentDto>>> GetAll()
        {
            var result = await _enrollmentService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/enrollments/{id}
        [HttpGet(Routes.Routes.Enrollments.GetEnrollmentById)]
        public async Task<ActionResult<EducationalPlatform.Application.DTOs.Enrollments.EnrollmentDto>> GetById(Guid enrollmentId)
        {
            var result = await _enrollmentService.GetByIdAsync(enrollmentId);
            return Ok(result);
        }

        // PUT: api/enrollments/{id}
        [HttpPut(Routes.Routes.Enrollments.UpdateEnrollment)]
        public async Task<ActionResult<EducationalPlatform.Application.DTOs.Enrollments.EnrollmentDto>> Update(
            Guid enrollmentId,
            [FromBody] EducationalPlatform.Application.DTOs.Enrollments.UpdateEnrollmentDto request)
        {
            var result = await _enrollmentService.UpdateAsync(enrollmentId, request);
            return Ok(result);
        }

        // DELETE: api/enrollments/{id}
        [HttpDelete(Routes.Routes.Enrollments.DeleteEnrollment)]
        public async Task<IActionResult> Delete(Guid enrollmentId)
        {
            await _enrollmentService.DeleteAsync(enrollmentId);
            return NoContent();
        }
    }
}
