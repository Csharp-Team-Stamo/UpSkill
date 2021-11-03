using System.Collections.Generic;

namespace UpSkill.Infrastructure.Models.Account
{
    public class UserConfirmPasswordResponseModel
    {
        public bool IsSuccessful { get; set; }

        public ICollection<string> Errors { get; set; } = new List<string>();
    }
}
