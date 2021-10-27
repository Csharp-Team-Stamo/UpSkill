using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace UpSkill.ClientSide.Infrastructure.Extensions
{
    public static class AuthenticationStateExtension
    {
        public static async Task<string> Role(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
        }

        public static async Task<string> Id(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Claims.FirstOrDefault(x => x.Type == "Id").Value;
        }

        public static async Task<string> Name(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        }

        public static async Task<string> Company(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Claims.FirstOrDefault(x => x.Type == "Company").Value;
        }

        public static async Task<string> Email(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        }

        public static async Task<string> Username(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Identity.Name;
        }

        public static async Task<bool> IsAuthenticated(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Identity.IsAuthenticated;
        }
    }
}
