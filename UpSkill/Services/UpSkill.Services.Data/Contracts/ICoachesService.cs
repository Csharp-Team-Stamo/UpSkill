namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Coach;

    public interface ICoachesService
    {
        CoachesListingCatalogModel GetAll(string ownerId);

        Task<CoachDescriptionModel> GetByIdAsync(string coachId);

        CoachesListingCatalogModel GetAllByOwnerId(string userId);

        Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId);

        Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string ownerId);
    }

}
