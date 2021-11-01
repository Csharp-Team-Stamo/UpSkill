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
            return result.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }

        public static async Task<string> Id(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
        }

        public static async Task<string> FullName(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Claims.FirstOrDefault(x => x.Type == "FullName")?.Value;
        }

        public static async Task<string> CompanyId(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Claims.FirstOrDefault(x => x.Type == "CompanyId")?.Value;
        }

        public static async Task<string> CompanyName(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Claims.FirstOrDefault(x => x.Type == "CompanyName")?.Value;
        }

        public static async Task<string> Email(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }

        public static async Task<string> Username(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Identity?.Name;
        }

        public static async Task<bool> IsAuthenticated(this Task<AuthenticationState> authState)
        {
            var result = await authState;
            return result.User.Identity != null && result.User.Identity.IsAuthenticated;
        }
    }
}
