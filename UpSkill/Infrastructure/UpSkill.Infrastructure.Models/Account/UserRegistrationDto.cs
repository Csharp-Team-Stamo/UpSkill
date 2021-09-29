using System.ComponentModel.DataAnnotations;

namespace UpSkill.Infrastructure.Models.Account
{
    public class UserRegistrationDto
    {
        public string FullName { get; set; }

        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
