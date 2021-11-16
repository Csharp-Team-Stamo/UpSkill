namespace UpSkill.Api.Controllers
{
    using Infrastructure.Models.ApplicationUser;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    [Route("[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IApplicationUserService applicationUserService;

        public ApplicationUsersController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        [HttpGet("GetById/{userId}")]
        public EditApplicationUserModel GetById(string userId)
        {
            return applicationUserService.GetById(userId);
        }
    }
}
