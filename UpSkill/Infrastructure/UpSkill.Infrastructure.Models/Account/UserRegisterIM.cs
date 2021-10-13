using System.ComponentModel.DataAnnotations;

namespace UpSkill.Infrastructure.Models.Account
{
    public class UserRegisterIM
    {
        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
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
