namespace UpSkill.Infrastructure.Models.Account
{
    public class AuthenticationResponseModel
    {
        public bool AuthIsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
