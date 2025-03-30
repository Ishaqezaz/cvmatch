using System;
using api.Dtos.ProfileSkill;
using api.Dtos.ProfileLocation;

namespace api.Dtos.Profile
{
    public class ProfileResponseDto
    {
        public string City { get; set; } = null!;

        public List<ProfileSkillResponseDto> ProfileSkills { get; set; } = new();

        public List<ProfileLocResponseDto> ProfileLocations { get; set; } = new();
    }
}