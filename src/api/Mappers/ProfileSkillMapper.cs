using System;
using MapperProfile = AutoMapper.Profile;
using api.Dtos.ProfileSkill;
using api.Models;

namespace api.Mappers
{
    public class ProfileSkillMapper : MapperProfile
    {
        public ProfileSkillMapper()
        {
            CreateMap<ProfileSkillCreateDto, ProfileSkill>();
            CreateMap<ProfileSkill, ProfileSkillResponseDto>();
            CreateMap<ProfileSkillUpdateDto, ProfileSkill>();
        }
    }
}