﻿namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Models.Company;
    using UpSkill.Services.Data.Admin.Contracts;

    public class CompanyController : AdminController
    {
        private readonly IAdminCompanyService companyService;

        public CompanyController(IAdminCompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody]CompanyCreateInputModel input)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest("Please fill all required fields.");
            }

            var createResult = await this.companyService
                .Create(input);

            if(createResult == null)
            {
                return StatusCode(500);
            }

            return StatusCode(201);
        }
    }
}
