using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Models.Identities
{
    public class Login
    {
        [Required(ErrorMessage = "User name is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }

    public class ChangePassword : Login
    {
        public string PasswordOld { get; set; } = string.Empty;
    }

    public class MainRole
    {
        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
