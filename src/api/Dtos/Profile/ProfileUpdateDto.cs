using System;
using api.Dtos.ProfileLocation;
using api.Dtos.ProfileSkill;

namespace api.Dtos.Profile
{
    public class ProfileUpdateDto
    {
        public string? CVPath { get; set; }

        public string? City { get; set; }

        public List<ProfileSkillCreateDto> ProfileSkills { get; set; } = new();

        public List<ProfileLocCreateDto> ProfileLocations { get; set; } = new();
    }
}