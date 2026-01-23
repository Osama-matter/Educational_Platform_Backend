using EducationalPlatform.Application.DTOs.Auth;
using EducationalPlatform.Application.Interfaces;
using EducationalPlatform.Application.Interfaces.Security;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Role = UserRole.Student
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errorDetails = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new System.Exception($"User creation failed: {errorDetails}");
            }

            return new UserDto
            {
                Email = user.Email,
                Token = await _jwtTokenService.GenerateTokenAsync(user)
            };
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null)
            {
                throw new System.Exception("Invalid email");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                throw new System.Exception("Invalid password");
            }

            if (user.Email is null)
            {
                throw new System.Exception("User email is missing");
            }

            return new UserDto
            {
                Email = user.Email,
                Token = await _jwtTokenService.GenerateTokenAsync(user)
            };
        }

        public async Task<UserDto> RegisterAdminAsync(RegisterAdminDto registerAdminDto)
        {
            var user = new User
            {
                UserName = registerAdminDto.Username,
                Email = registerAdminDto.Email,
                FirstName = registerAdminDto.FirstName,
                LastName = registerAdminDto.LastName,
                Role = UserRole.Admin
            };

            var result = await _userManager.CreateAsync(user, registerAdminDto.Password);

            if (!result.Succeeded)
            {
                var errorDetails = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new System.Exception($"Admin creation failed: {errorDetails}");
            }

            return new UserDto
            {
                Email = user.Email,
                Token = await _jwtTokenService.GenerateTokenAsync(user)
            };
        }

        public Task LogoutAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<UserDetailsDto> GetUserDetailsAsync()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new System.Exception("User not authenticated or email claim is missing.");
            }

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                throw new System.Exception("User not found.");
            }

            return new UserDetailsDto
            {
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
