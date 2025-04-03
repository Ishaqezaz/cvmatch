using System;
using System.ComponentModel.DataAnnotations;
using api.Dtos.ProfileLocation;
using api.Dtos.ProfileSkill;


namespace api.Dtos.Profile
{
    public class ProfileCreateDto
    {
        [Required]
        public string? CVPath { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string? City { get; set; }
        public List<ProfileSkillCreateDto>? Skills { get; set; } // derived
        public List<ProfileLocCreateDto>? Locations { get; set; } // derived and calculated from city
    }
}