
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UpSkill.Api.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UpSkill.Data;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Account;

    [Route("/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext dbContext;

        public AccountsController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto input)
        {
            if (input == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            //TODO
            // Check if email is available

            Company company = this.dbContext.Companies.FirstOrDefault(x => x.Name == input.CompanyName);

            if (company == null)
            {
                company = new Company
                {
                    Name = input.CompanyName,
                };
            }

            this.dbContext.Add(company);
            this.dbContext.SaveChanges();

            var user = new ApplicationUser 
            {
                UserName = input.Email,
                Email = input.Email,
                FullName = input.FullName,
                Company = company,
            };

            var result = await _userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            return StatusCode(201);
        }



    }
}
