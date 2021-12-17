namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Lecture;

    public interface ILectureService
    {
        Task<LectureDetailsModel> GetLectureById(int courseId, int lectureId);
    }
}
