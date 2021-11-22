namespace UpSkill.ClientSide.Infrastructure.Services.Contracts
{
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Common.Pagination.Coaches;
    using UpSkill.Infrastructure.Models.Coaches;

    public interface ICoachesService
    {
        Task<CoachesListingCatalogModel> GetAllAsync(string userId, CoachesParameters parameters);

        Task<CoachesListingCatalogModel> GetAllByOwnerIdAsync(string userId, CoachesParameters parameters);

        Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId);

        Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string userId);
    }
}
