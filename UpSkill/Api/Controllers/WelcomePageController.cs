namespace UpSkill.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Models.WelcomePage;
    using UpSkill.Services.Data.Contracts;
    using Infrastructure.Common;


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

            bool emailExists = await this.accountsService
                                         .EmailExists(input.Email);

            if(emailExists)
            {
                return BadRequest("You already have an UpSkill account.");
            }

            var textMessage = string.Format(GlobalConstants.demoMessage, input.Name);
            var messageAsHtml = string.Format(GlobalConstants.demoMessageHtml, input.Name);
            var subject = GlobalConstants.demoSubject;

            await this.emailSender.SendMailAsync(subject, input.Email, input.Name, textMessage, messageAsHtml);

            return StatusCode(200);
        }
    }
}
