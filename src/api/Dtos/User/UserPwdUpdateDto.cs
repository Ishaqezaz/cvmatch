using System;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class UserPwdUpdateDto
    {
        [Required]
        public required string Password { get; set; } // old pwd

        [Required]
        [MinLength(8)]
        public required string NewPassword { get; set; }
    }
}