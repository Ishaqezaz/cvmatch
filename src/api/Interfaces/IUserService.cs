using System;
using api.Common;
using api.Dtos.User;


namespace api.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<UserResponseDto>> UpdateUserAsync(int userId, UserUpdateDto dto);
        Task<ServiceResponse<UserResponseDto>> GetUserAsync(int userId);
        Task<ServiceResponse<UserResponseDto>> DeleteUserAsync(int userId);
        Task<ServiceResponse<UserResponseDto>> UpdateUserPasswordAsync(int userId, UserPwdUpdateDto dto);
    }
}