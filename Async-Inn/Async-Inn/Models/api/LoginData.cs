using System.ComponentModel.DataAnnotations;

namespace Async_Inn.Models.api
{
    public class LoginData
    {
        [Required(ErrorMessage = "The Username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password is required!")]
        public string Password { get; set; }
    }
}
