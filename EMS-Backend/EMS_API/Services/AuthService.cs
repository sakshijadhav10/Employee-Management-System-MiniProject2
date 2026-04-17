using EMS.API.Data;
using EMS.API.DTOs.Auth;
using EMS.API.Jwt;
using EMS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EMS.API.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(AppDbContext context,ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;

        }

        public async Task<AuthResponseDto?> Login(AuthRequestDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Username = user.Username,
                Role = user.Role,
                Token = token
            };
        }
        //REGISTER
        public async Task<(bool Success, string Message)> Register(AuthRequestDto dto)
        {
            var exists = await _context.Users
                .AnyAsync(u => u.Username == dto.Username);

            if (exists)
                return (false, "Username already exists");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new AppUser
            {
                Username = dto.Username,
                PasswordHash = hashedPassword,
                Role = string.IsNullOrEmpty(dto.Role) ? "Viewer" : dto.Role,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (true, "User registered successfully");
        }
    } 
    }
