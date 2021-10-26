namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
    using Infrastructure.Models.AddEmployeeModal;
    using Microsoft.AspNetCore.Mvc;

    [Route("/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpPost("PostEmployeesCollection")]
        public ICollection<AddEmployeeFormModel> PostEmployeesCollection(ICollection<AddEmployeeFormModel> employees)
        {
            return null;
        }
    }
}
