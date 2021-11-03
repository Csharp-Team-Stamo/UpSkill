namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("GetAllNames")]
        public ICollection<string> GetAllNames()
        {

        }
    }
}
