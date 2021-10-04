﻿
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UpSkill.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Models;
    using Infrastructure.Models.Account;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Services.Data.Contracts;

    [Route("/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly string unauthorizedErrorMessage = "Either you Email, or your Password were not correct.";
        private readonly IAccountsService accountService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfigurationSection jwtSettings;

        public AccountsController(
            IAccountsService accountService,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {

            this.accountService = accountService;
            this.userManager = userManager;
            this.jwtSettings = configuration.GetSection("JWTSettings");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto input)
        {
            if (input == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!this.accountService.IsEmailAvailable(input.Email))
            {
                return BadRequest("This email is already taken!");
            }

            var result = await this.accountService
                                   .Register(input.FullName, input.Email, input.Password, input.CompanyName);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpPost("Login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(
            [FromBody] UserAuthenticationDto userData)
        {
            var user = await this.userManager
                .FindByNameAsync(userData.Email);

            if (user == null || 
                !await this.userManager
                           .CheckPasswordAsync(user, userData.Password))
            {
                var unauthorizedResponse = new AuthenticationResponseDto
                {
                    ErrorMessage = this.unauthorizedErrorMessage
                };

                return Unauthorized(unauthorizedResponse);
            }

            var userToken = GetToken(user);

            var authenticationResponse = new AuthenticationResponseDto
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

        private List<Claim> GetClaims(IdentityUser user)
            => new ()
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

        private JwtSecurityToken GenerateTokenOptions(
            SigningCredentials signingCredentials, 
            List<Claim> claims)
            => new JwtSecurityToken(
                issuer: this.jwtSettings["validIssuer"],
                audience: this.jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(this.jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials);
    }
}
