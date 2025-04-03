using System;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Common.Extensions;
using api.Common;
using api.Dtos.User;
using Microsoft.AspNetCore.Authorization;


namespace api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize] // must be logged in
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        public UserController(IUserService user)
        {
            _user = user;
        }

        [HttpPatch()]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateDto dto)
        {
            var userId = User.GetUserId();
            var response = await _user.UpdateUserAsync(userId, dto);

            if (response.ErrorCode == ServiceErrorCode.NotFound) // either this or unexpected
                return NotFound(response);

            if (!response.IsSuccess)
                return StatusCode(500, response);

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetUserAsync()
        {
            var userId = User.GetUserId();
            var response = await _user.GetUserAsync(userId);

            if (response.ErrorCode == ServiceErrorCode.NotFound)
                return NotFound(response);

            if (!response.IsSuccess)
                return StatusCode(500, response);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync()
        {
            var userId = User.GetUserId();
            var response = await _user.DeleteUserAsync(userId);

            if (response.ErrorCode == ServiceErrorCode.NotFound)
                return NotFound(response);
            
            if (!response.IsSuccess)
                return StatusCode(500, response);

            return Ok(response);
        }
        
        [HttpPut("password")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UserPwdUpdateDto dto)
        {
            int userId = User.GetUserId();
            var response = await _user.UpdateUserPasswordAsync(userId, dto);

            if(response.ErrorCode == ServiceErrorCode.NotFound) // either this
                return NotFound(response);
            
            if(response.ErrorCode == ServiceErrorCode.Unauthorized ) // or this
                return BadRequest(response);

            if (!response.IsSuccess) // or unexpected
                return StatusCode(500, response);

            return Ok(response);
        }
    }
}