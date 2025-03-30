using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class ProfileLocation
    {
        [Key]
        public int ProfileLocationId { get; set; }

        [Required]
        public int ProfileId { get; set; }// profile specific, no orphan

        [Required]
        public Profile Profile { get; set; } = null!;

        [Required]
        public string Location { get; set; } = string.Empty;
    }
}