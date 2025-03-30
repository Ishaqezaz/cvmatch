using System;
using api.Dtos.Profile;
using MapperProfile = AutoMapper.Profile;
using api.Models;

namespace api.Mappers
{
    public class ProfileMapper : MapperProfile
    {
        public ProfileMapper()
        {
            CreateMap<ProfileCreateDto, Profile>();
            CreateMap<Profile, ProfileResponseDto>();
            CreateMap<ProfileUpdateDto, Profile>();
        }
    }
}