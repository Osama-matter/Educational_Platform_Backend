using EducationalPlatform.Infrastructure.Services.FawaterkServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.API.Controllers.Fawaterak
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/fawaterak")]
    public class FawaterakCallbacksController : ControllerBase
    {
        [HttpGet("payment-success")]
        public IActionResult PaymentSuccess()
        {
            // Redirect back to the frontend success page
            return Redirect("https://localhost:7154/Payment/Success");
        }

        [HttpGet("payment-failure")]
        public IActionResult PaymentFailure()
        {
            // Redirect back to the frontend failure page
            return Redirect("https://localhost:7154/Payment/Failure");
        }
    }
}
