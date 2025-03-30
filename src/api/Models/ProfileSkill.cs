using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class ProfileSkill
    {
        [Key]
        public int ProfileSkillId { get; set; }

        [Required]
        public int ProfileId { get; set; } // specific to profile, no orphan

        [Required]
        public Profile Profile { get; set; } = null!;

        [Required]
        public string Skill { get; set; } = string.Empty;
    }
}