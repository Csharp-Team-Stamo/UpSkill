namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.Coach;
    using Infrastructure.Common.Pagination;
    using Infrastructure.Models.Dashboard;
    using Paging;

    public interface ICoachesService
    {
        Task<CoachDescriptionModel> GetByIdAsync(string coachId);

        Task<CoachesListingCatalogModel> GetAllByOwnerIdAsync(string ownerId);

        Task<CoachesListingCatalogModel> GetAllByEmployeeIdAsync(string ownerId, string userId);

        Task<ICollection<CoachInListCatalogModel>> GetAllWithExistingSessionsAsync(string employeeId);

        Task<CoachesListingCatalogModel> GetAllAsync(string ownerId);

        Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId);

        Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string ownerId);

        Task<PagedList<CoachDashboardStatItemModel>> GetDashboardCoaches(string ownerId, int month,
            TableEntityParameters parameters);

    }

}
