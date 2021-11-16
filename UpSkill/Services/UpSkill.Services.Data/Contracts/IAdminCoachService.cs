namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Coach;

    public interface IAdminCoachService
    {
        Task<Coach> Create(CoachCreateInputModel coachInput);
        Task<Coach> GetCoach(string id);
        Task<CoachEditInputModel> GetCoachEditModel(string id);
        Task<CoachDetailsServiceModel> GetCoachDetails(string id);
        Task<IEnumerable<AdminCoachListingServiceModel>> GetAll();
        Task<int?> ExecuteEdit(CoachEditInputModel editInput);
        Task<int?> Delete(string id);
    }
}
