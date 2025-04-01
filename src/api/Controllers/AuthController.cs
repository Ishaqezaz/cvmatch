using System;
using api.Dtos.Auth;
using api.Dtos.User;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController] // includes model validator
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth; // delegate tasks to auth service
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
        {
            var response = await _auth.CreateUserAsync(dto);
            
            if(!response.IsSuccess)
                return Conflict(response);

            return Created(string.Empty, response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _auth.LoginUserAsync(dto);
            
            if(!response.IsSuccess)
                return BadRequest(response);

            return Ok(new { token = response.Data });
        }
    }
}