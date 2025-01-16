using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Models.Identities
{
    public class Register
    {
        public string Fullname { get; set; } = string.Empty;

        [Required(ErrorMessage = "User name is required")]
        public string Username { get; set; } = string.Empty;

        public string MainRoleId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
    }
}
