using System;
using System.ComponentModel.DataAnnotations;
using api.Common.Extensions;

// minimum to create profile
namespace api.Dtos.Profile
{
    public class ProfileCreateDto
    {
        [Required]
        [AllowedExtension([".pdf"])]
        public IFormFile CV { get; set; } = null!;
    }
}