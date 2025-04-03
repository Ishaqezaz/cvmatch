using System;
using api.Dtos.ProfileLocation;
using api.Dtos.ProfileSkill;


namespace api.Dtos.Profile
{
    public class ProfileUpdateDto
    {
        public string? CVPath { get; set; }
        public string? City { get; set; }
        public List<ProfileSkillUpdateDto>? Skills { get; set; }
        public List<ProfileLocUpdateDto>? Locations { get; set; }
    }
}