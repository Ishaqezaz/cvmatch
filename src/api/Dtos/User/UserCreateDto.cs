using System;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(40)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }
    }
}