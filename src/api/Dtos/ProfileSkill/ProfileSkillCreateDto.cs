using System;
using System.ComponentModel.DataAnnotations;


namespace api.Dtos.ProfileSkill
{
    public class ProfileSkillCreateDto
    {
        [Required]
        [MaxLength(20)]
        public required string Skill { get; set; } = string.Empty;
    }
}