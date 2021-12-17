namespace UpSkill.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using Infrastructure.Models.Lecture;
    using Contracts;

    public class LectureService : ILectureService
    {
        private readonly IDeletableEntityRepository<Lecture> lectureRepo;

        public LectureService(IDeletableEntityRepository<Lecture> lectureRepo)
        {
            this.lectureRepo = lectureRepo;
        }

        public async Task<LectureDetailsModel> GetLectureById(
            int courseId, int lectureId)
        {
            var lecture = await this.lectureRepo
                .All()
                .Select(l => new LectureDetailsModel
                {
                    Id = l.Id,
                    CourseId = l.Course.Id,
                    Title = l.Title,
                    Description = l.Description,
                    VideoUrl = l.VideoUrl,
                    Resources = l.Resources
                    .Select(r => r.Title).ToList()
                })
                .FirstOrDefaultAsync(
                    l => l.CourseId == courseId &&
                    l.Id == lectureId);

            return lecture;
        }
    }
}
