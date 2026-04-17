using EMS.API.Data;
using EMS.API.DTOs;
using EMS.API.DTOs.Auth;
using EMS.API.Jwt;
using EMS.API.Models;
using EMS.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }
        // 🔹 REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthRequestDto dto)
        {
            var result = await _auth.Register(dto);

            if (!result.Success)
                return Conflict(new { message = result.Message });

            return Ok(new
            {
                success = true,
                message = result.Message
            });
        }
        // 🔹 LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequestDto dto)
        {
            var result = await _auth.Login(dto);

            if (result == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new
            {
                success = true,
                username = result.Username,
                role = result.Role,
                token = result.Token
            });
        }


    }
}
