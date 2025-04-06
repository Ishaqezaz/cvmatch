using System;
using System.ComponentModel.DataAnnotations;


namespace api.Common.Extensions
{
    public class AllowedExtensionAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? type, ValidationContext context)
        {
            var file = type as IFormFile;
            if (file is not null)
            {
                var ex = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(ex))
                {
                    return new ValidationResult("File extension is not allowed");
                }
            }
            return ValidationResult.Success;
        }
    }
}