using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class UserUpdate
    {
        [MaxLength(40)]
        public string? FirstName { get; set; }

        [MaxLength(40)]
        public string? LastName { get; set; }
    }
}