using System.ComponentModel.DataAnnotations;

namespace Async_Inn.Models.api
{
    public class RegisterUser
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
