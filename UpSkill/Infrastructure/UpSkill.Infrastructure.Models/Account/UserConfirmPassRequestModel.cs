using System.ComponentModel.DataAnnotations;

namespace UpSkill.Infrastructure.Models.Account
{
   public class UserConfirmPassRequestModel
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string ResetToken { get; set; }
    }
}
