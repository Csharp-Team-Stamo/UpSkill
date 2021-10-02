using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace UpSkill.ClientSide.Authentication
{
    public class UpSkillAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ICollection<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "John Doe"),
            new Claim(ClaimTypes.Role, "Administrator")
        };

        // For test purposes
        private readonly ClaimsPrincipal anonymousUser = new ();

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // But why?
            await Task.Delay(1500);

            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(this.claims, "testAuthType"));


            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(authenticatedUser)));
        }
    }
}
