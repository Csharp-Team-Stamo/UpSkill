namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Coach;

    public interface ICoachesService
    {
        Task<CoachDescriptionModel> GetByIdAsync(string coachId);

        CoachesListingCatalogModel GetAllByOwnerId(string ownerId);

        CoachesListingCatalogModel GetAllByEmployeeId(string ownerId, string userId);

        CoachesListingCatalogModel GetAll(string ownerId);

        Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId);

        Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string ownerId);
    }

}
