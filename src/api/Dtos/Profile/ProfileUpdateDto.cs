using System;
using api.Dtos.ProfileLocation;
using api.Dtos.ProfileSkill;

// no scan feature, manual update from user
namespace api.Dtos.Profile
{
    public class ProfileUpdateDto
    {
        public List<ProfileSkillUpdateDto>? Skills { get; set; }

        public List<ProfileLocUpdateDto>? Locations { get; set; }
    }
}