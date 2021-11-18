namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Admin;
    using UpSkill.Infrastructure.Models.Company;

    public interface IStatisticsService
    {
        Task<IEnumerable<MonthlyClient>> GetClientsStatistics(int? year);
        Task<IEnumerable<AdminCompanyListingModel>> GetAllClients();
        Task<AdminDasboardStatsServiceModel> GetDashboardStats();
    }
}
