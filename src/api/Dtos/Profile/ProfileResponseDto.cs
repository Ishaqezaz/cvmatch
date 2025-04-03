using System;
using api.Dtos.ProfileSkill;
using api.Dtos.ProfileLocation;


namespace api.Dtos.Profile
{
    public class ProfileResponseDto
    {
        public string City { get; set; } = string.Empty;
        public List<ProfileSkillResponseDto> Skills { get; set; } = new();
        public List<ProfileLocResponseDto> Locations { get; set; } = new();
    }
}