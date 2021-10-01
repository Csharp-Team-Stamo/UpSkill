namespace UpSkill.Infrastructure.Models.Account
{

using System.Collections.Generic;
    public class RegistrationResponseDto
    {
        public bool IsSuccessfulRegistration { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
