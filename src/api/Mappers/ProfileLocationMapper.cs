using System;
using MapperProfile = AutoMapper.Profile;
using api.Dtos.ProfileLocation;
using api.Models;

namespace api.Mappers
{
    public class ProfileLocationMapper : MapperProfile
    {
        public ProfileLocationMapper()
        {
            CreateMap<ProfileLocCreateDto, ProfileLocation>();
            CreateMap<ProfileLocation, ProfileLocResponseDto>();
            CreateMap<ProfileLocUpdateDto, ProfileLocation>();
        }
    }
}