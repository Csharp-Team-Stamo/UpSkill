namespace UpSkill.Api.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Administrator, ADMINISTRATOR")]
    [Area("Admin")]
    [Route("admin/[controller]")]
    [ApiController]
    public abstract class AdminController : ControllerBase
    {
    }
}
