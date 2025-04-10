using System;
using api.Common;
using api.Dtos.Auth;
using api.Dtos.User;
using api.Models;

namespace api.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<UserResponseDto>> CreateUserAsync(UserCreateDto dto);
        Task<ServiceResponse<string>> LoginUserAsync(LoginDto dto);
        string CreateJwtToken(User user);
    }
}