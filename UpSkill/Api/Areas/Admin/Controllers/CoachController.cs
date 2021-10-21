namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class CoachController : AdminController
    {
        public async Task<ActionResult> Create()
        {
            // TODO create CoachCreateInputModel & pass it [FromBody] to the ctor
            return StatusCode(201);
        }
    }
}
