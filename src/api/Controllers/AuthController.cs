using System;
using api.Dtos.Auth;
using api.Dtos.User;
using api.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController] // includes model validator
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {   
        private readonly IAuth _auth; // delegate tasks to auth service
        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
        {
            var response = await _auth.CreateUserAsync(dto);
            return Created(string.Empty, response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _auth.LoginUserAsync(dto);
            return Ok(new {token = response});
        }
    }
}