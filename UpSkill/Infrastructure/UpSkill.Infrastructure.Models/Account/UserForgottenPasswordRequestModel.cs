namespace UpSkill.Infrastructure.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    public class UserForgottenPasswordRequestModel
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
