﻿
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

    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Account;
    using UpSkill.Services.Data.Contracts;

    [Route("/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
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
        public async Task<IActionResult> Register([FromBody] UserRegisterIM input)
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
                return BadRequest(new RegisterResponseModel { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpPost("Login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(
        [FromBody] UserLoginIM userData)
        {
            var user = await this.userManager
                .FindByEmailAsync(userData.Email);

            if (user == null ||
                !await this.userManager
                           .CheckPasswordAsync(user, userData.Password))
            {
                var unauthorizedResponse = new LoginResponseModel
                {
                    ErrorMessage = "unauthorizedErrorMessage"
                };

                return Unauthorized(unauthorizedResponse);
            }

            var userToken = GetToken(user);

            var authenticationResponse = new LoginResponseModel
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
            //TODO --- NO IDEA!!!
            var claims = this.userManager.GetClaimsAsync(user);
           
            var claimsAsList = new List<Claim>(claims.Result);

            //var userNameClaim = new Claim(ClaimTypes.Name, user.FullName);
            //var userEmailClaim = new Claim(ClaimTypes.Email, user.Email);

            return claimsAsList;
        }

        private JwtSecurityToken GenerateTokenOptions(
            SigningCredentials signingCredentials,
            IList<Claim> claims)
            => new (
                issuer: this.jwtSettings["validIssuer"],
                audience: this.jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(this.jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials);

    }

}

