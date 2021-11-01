﻿namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Models.Course;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    public class CourseController : AdminController
    {
        private readonly IAdminCourseService courseService;
        private readonly IAdminCategoryService categoryService;

        public CourseController(
            IAdminCourseService courseService,
            IAdminCategoryService categoryService)
        {
            this.courseService = courseService;
            this.categoryService = categoryService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] CourseCreateInputModel input)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest("Please fill in all required fields.");
            }

            var createResult = await this.courseService.Create(input);

            if(createResult == null)
            {
                return StatusCode(500);
            }

            return StatusCode(201);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<AdminCourseListingServiceModel>>> All()
        {
            var allCourses = await this.courseService.All();

            if(allCourses.Any() == false)
            {
                return NotFound();
            }

            return new List<AdminCourseListingServiceModel>(allCourses);
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult<CourseDetailsServiceModel>> Details(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            var courseDetails = await this.courseService.GetCourseDetails(id);

            return courseDetails;
        }

        [HttpGet("Edit/{id}")]
        public async Task<ActionResult<CourseEditInputModel>> Edit(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Valid Course Id is required.");
            }

            var courseInDb = await this.courseService.GetCourse(id);

            if(courseInDb == null)
            {
                return NotFound();
            }

            // var allCategories = await this.categoryService.GetAll();

            var editModel = new CourseEditInputModel
            {
                Id = courseInDb.Id,
                AuthorCompany = courseInDb.CompanyLogoUrl,
                AuthorFullName = courseInDb.AuthorFullName,
                Name = courseInDb.Name,
                Description = courseInDb.Description,
                ImageUrl = courseInDb.ImageUrl,
                Price = courseInDb.Price,
                VideoUrl = courseInDb.VideoUrl,
                CategoryId = courseInDb.Category.Id,
                CategoryName = courseInDb.Category.Name
            };

            // editModel.AllCategories = await this.categoryService.GetAll();

            return editModel;
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromBody] CourseEditInputModel input)
        {
            if(input == null || 
                ModelState.IsValid == false || 
                input.Id <= 0)
            {
                return BadRequest("A valid Id is needed.");
            }

            var editResult = await this.courseService.Edit(input);

            if(editResult == null)
            {
                return StatusCode(500);
            }

            return StatusCode(200);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> SetDelete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A valid Id is required.");
            }

            var deleteResult = await this.courseService
                                         .SetDelete(id);

            if(deleteResult == null)
            {
                return StatusCode(500);
            }

            return StatusCode(200);
        }
    }
}
