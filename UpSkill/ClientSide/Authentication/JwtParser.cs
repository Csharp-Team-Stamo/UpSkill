namespace UpSkill.ClientSide.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.Json;

    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            ExtractClaimsFromJWT(claims, keyValuePairs);

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private static void ExtractClaimsFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
        {
            foreach (var claim in keyValuePairs)
            {
                Console.WriteLine($"{claim.Key} - ${claim.Value}");
            }


            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);
            keyValuePairs.TryGetValue("Id", out object id);
            keyValuePairs.TryGetValue("Company", out object companyId);
            keyValuePairs.TryGetValue(ClaimTypes.Email, out object email);
            keyValuePairs.TryGetValue(ClaimTypes.Name, out object name);

            if (roles != null)
            {
                var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');

                if (parsedRoles.Length > 1)
                {
                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            if (id != null)
            {
                claims.Add(new Claim("Id", id.ToString()?.Trim() ?? string.Empty));
            }


            if (companyId != null)
            {
                claims.Add(new Claim("CompanyId", companyId.ToString()?.Trim() ?? string.Empty));
            }

            if (email != null)
            {
                claims.Add(new Claim(ClaimTypes.Email, email.ToString()?.Trim() ?? string.Empty));
            }

            if (name != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, name.ToString()?.Trim() ?? string.Empty));
            }
        }
    }
}
