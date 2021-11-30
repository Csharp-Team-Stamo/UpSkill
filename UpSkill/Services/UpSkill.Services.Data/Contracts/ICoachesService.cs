namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.Coach;

    public interface ICoachesService
    {
        Task<CoachDescriptionModel> GetByIdAsync(string coachId);

        CoachesListingCatalogModel GetAllByOwnerId(string ownerId);

        CoachesListingCatalogModel GetAllByEmployeeId(string ownerId, string userId);

        CoachesListingCatalogModel GetAll(string ownerId);

        Task<ICollection<CoachInListCatalogModel>> GetAllWithExistingSessions(string employeeId);

        Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId);

        Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string ownerId);
    }

}
