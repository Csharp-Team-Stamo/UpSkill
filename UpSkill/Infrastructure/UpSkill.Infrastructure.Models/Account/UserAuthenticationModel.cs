namespace UpSkill.Infrastructure.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    public class UserAuthenticationModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
