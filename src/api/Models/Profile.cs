using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }

        public List<ProfileSkill> Skills { get; set; } = new();

        public List<ProfileLocation> Locations { get; set; } = new();

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; } = null!;
    }
}