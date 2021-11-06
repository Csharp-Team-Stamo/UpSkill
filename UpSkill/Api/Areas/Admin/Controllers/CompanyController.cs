namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UpSkill.Infrastructure.Models.Company;

    public class CompanyController : AdminController
    {
        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody]CompanyCreateInputModel input)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest("Please fill all required fields.");
            }

            await Task.Delay(0);
            return StatusCode(201);
        }
    }
}
