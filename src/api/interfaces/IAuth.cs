using System;
using api.Dtos.Auth;
using api.Dtos.User;
using api.Models;

namespace api.interfaces
{
    public interface IAuth
    {
        Task<UserResponseDto> CreateUserAsync(UserCreateDto dto);

        Task<string> LoginUserAsync(LoginDto dto);

        string CreateJwtToken(User user);
    }
}