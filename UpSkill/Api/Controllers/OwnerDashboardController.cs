namespace UpSkill.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Owner;
    using UpSkill.Services.Data.Contracts;

    [Route("/[controller]")]
    [ApiController]
    public class OwnerDashboardController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOwnerService ownerService;

        public OwnerDashboardController(
            UserManager<ApplicationUser> userManager,
            IOwnerService ownerService)
        {
            this.userManager = userManager;
            this.ownerService = ownerService;
        }

        [HttpGet("GetInvoiceInfo/{userId}/{monthNum}")]
        public async Task<ActionResult<OwnerInvoiceDetailsModel>> GetInvoiceInfo(
            string userId, int monthNum)
        {
            if(userId == null ||
                monthNum <= 0 ||
                monthNum > 12)
            {
                return BadRequest("Valid Owner Id and month number is required.");
            }

            var user = await this.userManager
                .FindByIdAsync(userId);

            if(user == null)
            {
                return BadRequest("Owner does not exist.");
            }

            var ownerId = await this.ownerService.GetOwnerId(user.Id);

            if(ownerId == null)
            {
                return BadRequest("The User is not an Owner.");
            }

            var invoiceInfo = await this.ownerService
                .GetInvoiceInfo(ownerId, monthNum);

            return invoiceInfo;
        }
    }
}
