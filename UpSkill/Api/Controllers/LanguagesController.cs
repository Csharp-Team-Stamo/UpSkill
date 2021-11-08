namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    [Route("[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguagesService languagesService;

        public LanguagesController(ILanguagesService languagesService)
        {
            this.languagesService = languagesService;
        }

        [HttpGet("GetAll")]
        public ICollection<string> GetAll()
        {
           return languagesService.GetAll();
        }
    }
}
