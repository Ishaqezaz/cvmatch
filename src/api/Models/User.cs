using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set;}

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set;} = string.Empty;

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string HashPassword { get; set; } = string.Empty;

        public int? ProfileId { get; set; } // orphan
        
        public Profile? Profile { get; set; }
    }
}