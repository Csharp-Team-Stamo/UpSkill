namespace UpSkill.Api.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Admin, ADMIN")]
    [Area("Admin")]
    [Route("area/[controller]")]
    [ApiController]
    public abstract class AdminController : ControllerBase
    {
    }
}
