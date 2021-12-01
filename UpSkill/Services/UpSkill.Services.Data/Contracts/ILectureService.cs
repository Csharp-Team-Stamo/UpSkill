namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Lecture;

    public interface ILectureService
    {
        Task<LectureDetailsModel> GetLectureById(int courseId, int lectureId);
    }
}
