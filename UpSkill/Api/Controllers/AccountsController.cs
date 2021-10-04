
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UpSkill.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Account;
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



    }
}
