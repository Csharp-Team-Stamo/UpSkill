namespace UpSkill.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using UpSkill.Infrastructure.Models.WelcomePage;
    using UpSkill.Services.Data.Contracts;

    [Route("/[controller]")]
    [ApiController]
    public class WelcomePageController : ControllerBase
    {
        private readonly IAccountsService accountsService;
        private readonly IEmailSender emailSender;

        public WelcomePageController(
            IAccountsService accountsService,
            IEmailSender emailSender)
        {
            this.accountsService = accountsService;
            this.emailSender = emailSender;
        }

        [HttpPost("RequestDemo")]
        public async Task<IActionResult> RequestDemo([FromBody] RequestDemoFromModel input)
        {
            if (input == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            if(this.accountsService.IsEmailAvailable(input.Email) == false)
            {
                return BadRequest("You already have an UpSkill account.");
            }

            var htmlMessage = "";

            await this.emailSender.SendEmailAsync(input.Email, "Your UpSkill Demo", htmlMessage);

            return StatusCode(200);
        }
    }
}
