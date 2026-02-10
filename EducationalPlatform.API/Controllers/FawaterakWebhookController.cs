using EducationalPlatform.Application.DTOs.FawaterkDTO;
using EducationalPlatform.Infrastructure.Services.FawaterkServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EducationalPlatform.API.Controllers;

/// <summary>
/// Webhook endpoints for Fawaterak payment notifications
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("api/fawaterak/webhooks")]
[Consumes("application/json")]
[Produces("application/json")]
public class FawaterakWebhooksController : ControllerBase
{
    private readonly IFawaterakPaymentService _payments;

    public FawaterakWebhooksController(IFawaterakPaymentService payments)
    {
        _payments = payments;
    }

    /// <summary>
    /// Handle successful payment notification from Fawaterak
    /// </summary>
    /// <param name="model">Payment webhook data with invoice details and verification hash</param>
    /// <returns>Confirmation message</returns>
    /// <response code="200">Webhook processed successfully</response>
    /// <response code="401">Invalid webhook signature</response>
    [HttpPost("paid_json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string>> WebhookPaid([FromBody] WebHookModel model)
    {
        try
        {
            var valid = _payments.VerifyWebhook(model);
            if (!valid)
            {
                Console.WriteLine("[DEBUG] Webhook signature verification failed");
                return Unauthorized();
            }

            if (model.Payload != null && Guid.TryParse(model.Payload.OrderId, out Guid enrollmentId))
            {
                using var scope = HttpContext.RequestServices.CreateScope();
                var enrollmentRepository = scope.ServiceProvider.GetRequiredService<EducationalPlatform.Application.Interfaces.Repositories.IEnrollmentRepository>();
                var userRepository = scope.ServiceProvider.GetRequiredService<EducationalPlatform.Application.Interfaces.Repositories.IUserRepository>();
                var courseRepository = scope.ServiceProvider.GetRequiredService<EducationalPlatform.Application.Interfaces.Repositories.ICourseRepository>();
                var emailService = scope.ServiceProvider.GetRequiredService<EducationalPlatform.Application.Interfaces.Services.IEmailService>();

                var enrollment = await enrollmentRepository.GetByIdAsync(enrollmentId);
                if (enrollment != null)
                {
                    enrollment.IsActive = true;
                    enrollment.PaymentStatus = "Paid";
                    await enrollmentRepository.UpdateAsync(enrollment);
                    Console.WriteLine($"[DEBUG] Enrollment {enrollmentId} activated and marked as Paid");

                    var user = await userRepository.GetByIdAsync(enrollment.StudentId);
                    var course = await courseRepository.GetByIdAsync(enrollment.CourseId);
                    if (user != null && course != null)
                    {
                        await emailService.SendEnrollmentEmailAsync(user.Email, user.UserName, course.Title);
                    }
                }
                else
                {
                    Console.WriteLine($"[DEBUG] Enrollment {enrollmentId} not found in database");
                }
            }
            else
            {
                Console.WriteLine("[DEBUG] Invalid or missing OrderId in Webhook payload");
            }

            return Ok("got it!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DEBUG] Webhook Error: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Handle payment cancellation notification from Fawaterak
    /// </summary>
    /// <param name="model">Cancellation webhook data with reference ID and verification hash</param>
    /// <returns>Acknowledgment of cancellation</returns>
    /// <response code="200">Cancellation webhook processed successfully</response>
    /// <response code="401">Invalid webhook signature</response>
    [HttpPost("cancel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult WebhookCancel([FromBody] CancelTransactionModel model)
    {
        var valid = _payments.VerifyCancelTransaction(model);
        if (!valid) return Unauthorized();

        // Handle the cancellation logic here

        return Ok();
    }

    /// <summary>
    /// Handle failed payment notification from Fawaterak
    /// </summary>
    /// <param name="model">Failed payment webhook data with reference ID and verification hash</param>
    /// <returns>Acknowledgment of failure</returns>
    /// <response code="200">Failure webhook processed successfully</response>
    /// <response code="401">Invalid webhook signature</response>
    [HttpPost("failed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult WebhookFaild([FromBody] CancelTransactionModel model)
    {
        var valid = _payments.VerifyCancelTransaction(model);
        if (!valid) return Unauthorized();

        // Handle the failed logic here

        return Ok();
    }
}