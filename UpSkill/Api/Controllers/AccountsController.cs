
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UpSkill.Api.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Data.Models;
    using Infrastructure.Models.Account;
    using UpSkill.Services.Data.Contracts;
    using UpSkill.Infrastructure.Common;

    [Route("/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService accountService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompanyService companyService;
        private readonly IConfigurationSection jwtSettings;

        public AccountsController(IAccountsService accountService, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, ICompanyService companyService)
        {

            this.accountService = accountService;
            this.userManager = userManager;
            this.companyService = companyService;
            this.jwtSettings = configuration.GetSection("JWTSettings");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationModel input)
        {
            if (input == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!this.accountService.IsEmailAvailable(input.Email))
            {
                return BadRequest("This email is already taken!");
            }

            var result = await this.accountService.Register(input.FullName, input.Email, input.Password, input.CompanyName);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseModel { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpPost("Request-reset-password")]
        public async Task<IActionResult> RequestResetPassword([FromBody] UserForgottenPasswordRequestModel input)
        {
            var user = await userManager.FindByEmailAsync(input.Email);

            if (user == null)
            {
                var error = "User with that email does not exist!";
                return BadRequest(error);
            }

            await accountService.ResetPassword(user, GlobalConstants.resetPasswordMessage);

            return Ok();
        }

        [HttpPost("Reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] UserConfirmPassRequestModel input)
        {
            if (!input.Password.Equals(input.ConfirmPassword))
            {
                return BadRequest("Password and confirm password do not match!");
            }

            var user = userManager.FindByEmailAsync(input.Email).Result;
            var escapedPlusSymbolToken = input.ResetToken.Replace(" ", "+");
            var result = await userManager.ResetPasswordAsync(user, escapedPlusSymbolToken, input.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(x => x.Description)
                    .ToList();

                return BadRequest(errors);
            }

            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                var confirmEmailToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await userManager.ConfirmEmailAsync(user, confirmEmailToken);
            }

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(
        [FromBody] UserAuthenticationModel userData)
        {
            var user = await this.userManager.FindByEmailAsync(userData.Email);
            var unauthorizedResponse = new UserAuthenticationResponseModel();


            if (user == null ||
                !await this.userManager
                           .CheckPasswordAsync(user, userData.Password))
            {
                unauthorizedResponse.ErrorMessage = "Username or password is incorrect!";
                return Unauthorized(unauthorizedResponse);
            }

            //TODO: remove in production evironment. Every user should confirm their email.
            var claims = await userManager.GetClaimsAsync(user);
            var role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            if (!user.EmailConfirmed && role == "Employee")
            {
                unauthorizedResponse.ErrorMessage = "Email is not confirmed.";
                return Unauthorized(unauthorizedResponse);
            }

            var userToken = GetToken(user);

            var authenticationResponse = new UserAuthenticationResponseModel
            {
                AuthIsSuccessful = true,
                Token = userToken
            };

            return Ok(authenticationResponse);
        }

        private string GetToken(ApplicationUser user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(this.jwtSettings["securityKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private IList<Claim> GetClaims(ApplicationUser user)
        {
            var claims = this.userManager.GetClaimsAsync(user);
            var claimsAsList = new List<Claim>(claims.Result);

            claimsAsList.Add(new Claim(ClaimTypes.Email, user.Email));
            claimsAsList.Add(new Claim("Id", user.Id));
            claimsAsList.Add(new Claim("FullName", user.FullName));
            claimsAsList.Add(new Claim("CompanyId", user.CompanyId.ToString()));
            claimsAsList.Add(new Claim("CompanyName", companyService.GetName(user.CompanyId)));

            return claimsAsList;
        }

        private JwtSecurityToken GenerateTokenOptions(
            SigningCredentials signingCredentials,
            IList<Claim> claims)
            => new JwtSecurityToken(
                issuer: this.jwtSettings["validIssuer"],
                audience: this.jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(this.jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials);
    }
}
