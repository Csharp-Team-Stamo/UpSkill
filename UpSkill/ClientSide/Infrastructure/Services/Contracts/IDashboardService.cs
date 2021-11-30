
namespace UpSkill.ClientSide.Infrastructure.Services.Contracts
{

using System.Threading.Tasks;
    using UpSkill.Infrastructure.Common.Pagination;
    using UpSkill.Infrastructure.Models.Dashboard;

    public interface IDashboardService
    {
        Task<PagingResponse<CourseDashboardStatItemModel>> GetOwnerCoursesStatsAsync(string ownerId, string month, TableEntityParameters parameters);

    }
}
