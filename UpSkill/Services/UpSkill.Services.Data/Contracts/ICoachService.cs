namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.CoachDescriptionModal;
    using Infrastructure.Models.Coaches;
    using UpSkill.Infrastructure.Common.Pagination.Coaches;

    public interface ICoachService
    {
        CoachesListingCatalogModel GetAll(string userId, CoachesParameters parameters);

        Task<CoachDescriptionModel> GetByIdAsync(string coachId);

        CoachesListingCatalogModel GetAllByOwnerId(string userId, CoachesParameters parameters);

        Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId);

        Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string userId);
    }

}
