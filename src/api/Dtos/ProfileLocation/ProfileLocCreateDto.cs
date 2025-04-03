using System;
using System.ComponentModel.DataAnnotations;


namespace api.Dtos.ProfileLocation
{
    public class ProfileLocCreateDto
    {
        [Required]
        [MaxLength(20)]
        public required string Location { get; set; } = string.Empty;
    }
}