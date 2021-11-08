namespace UpSkill.Infrastructure.Models.Account
{
    public class UserAuthenticationResponseModel
    {
        public bool AuthIsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
