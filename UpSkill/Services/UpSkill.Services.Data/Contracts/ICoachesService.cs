namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.CoachDescriptionModal;
    using Infrastructure.Models.Coaches;

    public interface ICoachesService
    {
        CoachesListingCatalogModel GetAll(string userId);

        Task<CoachDescriptionModel> GetByIdAsync(string coachId);

        CoachesListingCatalogModel GetAllByOwnerId(string userId);

        Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId);

        Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string userId);
    }

}
