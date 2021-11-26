namespace UpSkill.ClientSide.Infrastructure.Services.Contracts
{
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Coach;

    public interface ICoachesService
    {
        Task<CoachesListingCatalogModel> GetAllAsync(string ownerId);

        Task<CoachesListingCatalogModel> GetAllByOwnerIdAsync(string ownerId);
        Task<CoachesListingCatalogModel> GetAllByEmployeeIdAsync(string ownerId, string userId);
        Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId);

        Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string ownerId);
    }
}
