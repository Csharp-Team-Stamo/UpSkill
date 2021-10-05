namespace UpSkill.Infrastructure.Models.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthenticationResponseDto
    {
        public bool AuthIsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
