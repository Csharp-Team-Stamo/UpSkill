
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UpSkill.Api.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UpSkill.Data;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Account;
    using UpSkill.Services.Data;
    using UpSkill.Services.Data.Contracts;

    [Route("/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService accountService;

        public AccountsController(IAccountsService accountService)
        {
            this.accountService = accountService;
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


            var result = await this.accountService.Register(input.FullName, input.Email, input.Password, input.CompanyName);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            return StatusCode(201);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!this.accountService.UserExists(user.Email))
            {
                return BadRequest("Wrong username or password.");
            }

            var isPasswordCorrect = await accountService.IsPasswordCorrect(user.Email, user.Password);

            if (!isPasswordCorrect)
            {
                return BadRequest("Wrong username or password.");
            }

            var loggingInUserProcess = await accountService.Login(user.Email);

            return StatusCode(200, "Login Successful");
        }
    }
}
