using EducationalPlatform.Application.DTOs;
using EducationalPlatform.Application.Interfaces;
using EducationalPlatform.Application.Interfaces.Security;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
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
                throw new System.Exception("User creation failed");
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
                throw new System.Exception("Admin creation failed");
            }

            return new UserDto
            {
                Email = user.Email,
                Token = await _jwtTokenService.GenerateTokenAsync(user)
            };
        }
    }
}
