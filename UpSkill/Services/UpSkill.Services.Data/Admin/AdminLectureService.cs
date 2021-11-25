using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UpSkill.Data.Common.Repositories;
using UpSkill.Data.Models;
using UpSkill.Infrastructure.Models.Lecture;
using UpSkill.Services.Data.Admin.Contracts;

namespace UpSkill.Services.Data.Admin
{
    public class AdminLectureService : IAdminLectureService
    {
        private readonly IDeletableEntityRepository<Course> courseRepo;
        private readonly IDeletableEntityRepository<Lecture> lectureRepo;

        public AdminLectureService(
            IDeletableEntityRepository<Course> courseRepo,
            IDeletableEntityRepository<Lecture> lectureRepo)
        {
            this.courseRepo = courseRepo;
            this.lectureRepo = lectureRepo;
        }

        public async Task<int?> Create(LectureCreateServiceModel input)
        {
            var lecture = await CreateLecture(input);

            if(lecture == null)
            {
                return null;
            }

            await this.lectureRepo.AddAsync(lecture);
            var createResult = await this.lectureRepo.SaveChangesAsync();

            return createResult <= 0 ?
                null :
                lecture.Id;
        }

        private async Task<Course> GetCourse(int courseId)
            => await this.courseRepo
                         .All()
                         .FirstOrDefaultAsync(c => c.Id == courseId);

        private async Task<Lecture> CreateLecture(LectureCreateServiceModel input)
        {
            var course = await GetCourse(input.CourseId);

            if (course == null)
            {
                return null;
            }

            return new Lecture
            {
                Course = course,
                CourseId = course.Id,
                Title = input.Title,
                Description = input.Description,
                VideoUrl = input.VideoUrl
            };
        }
    }
}
