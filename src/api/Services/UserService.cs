using System;
using api.Common;
using api.Data;
using api.Interfaces;
using AutoMapper;
using api.Dtos.User;
using Microsoft.AspNetCore.Identity;
using api.Models;


namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<User> _pwd;
        public UserService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _pwd = new PasswordHasher<User>();
        }

        public async Task<ServiceResponse<UserResponseDto>> UpdateUserAsync(int userId, UserUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null)
                return ServiceResponse<UserResponseDto>.Fail("Could not find user", ServiceErrorCode.NotFound);

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                user.FirstName = dto.FirstName;

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                user.LastName = dto.LastName;

            await _context.SaveChangesAsync();
            var data = _mapper.Map<UserResponseDto>(user);

            return ServiceResponse<UserResponseDto>.Success("User updated", data);
        }

        public async Task<ServiceResponse<UserResponseDto>> GetUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null)
                return ServiceResponse<UserResponseDto>.Fail("Could not find user", ServiceErrorCode.NotFound);

            var data = _mapper.Map<UserResponseDto>(user);

            return ServiceResponse<UserResponseDto>.Success("User fetched", data);
        }

        public async Task<ServiceResponse<UserResponseDto>> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null)
                return ServiceResponse<UserResponseDto>.Fail("Could not find user", ServiceErrorCode.NotFound);

            var data = _mapper.Map<UserResponseDto>(user);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            
            return ServiceResponse<UserResponseDto>.Success("Deleted user", null);
        }

        public async Task<ServiceResponse<UserResponseDto>> UpdateUserPasswordAsync(int userId, UserPwdUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null)
            
                return ServiceResponse<UserResponseDto>.Fail("Could not find user", ServiceErrorCode.NotFound);
            
            var verify = _pwd.VerifyHashedPassword(user, user.HashPassword, dto.Password);

            if(verify == PasswordVerificationResult.Failed)
                return ServiceResponse<UserResponseDto>.Fail("Wrong credentials", ServiceErrorCode.Unauthorized);

            user.HashPassword = _pwd.HashPassword(user, dto.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return ServiceResponse<UserResponseDto>.Success("Password updated", null);
        }
    }
}