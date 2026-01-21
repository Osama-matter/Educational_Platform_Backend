using EducationalPlatform.Application.DTOs.Auth;
using EducationalPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EducationalPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            try
            {
                var userDto = await _authService.RegisterAsync(registerDto);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                var userDto = await _authService.LoginAsync(loginDto);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("register-admin")]
        public async Task<ActionResult<UserDto>> RegisterAdmin(RegisterAdminDto registerAdminDto)
        {
            try
            {
                var userDto = await _authService.RegisterAdminAsync(registerAdminDto);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
