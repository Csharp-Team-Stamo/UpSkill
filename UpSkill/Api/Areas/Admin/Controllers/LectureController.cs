namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UpSkill.Infrastructure.Models.Lecture;
    using UpSkill.Services.Data.Admin.Contracts;

    public class LectureController : AdminController
    {
        private readonly IAdminLectureService adminLectureService;

        public LectureController(IAdminLectureService adminLectureService)
        {
            this.adminLectureService = adminLectureService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(LectureCreateInputModel input)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest("Please add all required data.");
            }
            var serviceModel = new LectureCreateServiceModel
            {
                CourseId = input.CourseId,
                Title = input.Title,
                Description = input.Description,
                VideoUrl = input.VideoUrl
            };

            var createResult = await this.adminLectureService.Create(serviceModel);

            return createResult == null ?
                StatusCode(500) :
                StatusCode(201);
        }
    }
}
