namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class CompanyController : AdminController
    {
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            await Task.Delay(0);
            return StatusCode(201);
        }
    }
}
