namespace UpSkill.Infrastructure.Models.Account
{

using System.Collections.Generic;
    public class RegisterResponseModel
    {
        public bool IsSuccessfulRegistration { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
