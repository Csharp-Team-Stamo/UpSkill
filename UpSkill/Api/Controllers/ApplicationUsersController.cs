namespace UpSkill.Api.Controllers
{
    using System.Threading.Tasks;
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

        [HttpGet("GetByIdAsync/{userId}")]
        public async Task<EditApplicationUserModel> GetByIdAsync(string userId)
        {
            return await applicationUserService.GetByIdAsync(userId);
        }

        [HttpPut("UpdateAsync")]
        public async Task UpdateAsync(EditApplicationUserModel model)
        {
           await applicationUserService.UpdateUserAsync(model);
        }
    }
}
