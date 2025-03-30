using System;
using MapperProfile = AutoMapper.Profile;
using api.Dtos.User;
using api.Models;

namespace api.Mappers
{
    public class UserMapper : MapperProfile
    {
        public UserMapper()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserResponseDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserPwdUpdateDto, User>();
        }
    }
}