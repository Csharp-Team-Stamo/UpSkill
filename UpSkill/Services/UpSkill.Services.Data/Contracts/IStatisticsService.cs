namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.Admin;
    using Infrastructure.Models.Company;

    public interface IStatisticsService
    {
        Task<IEnumerable<MonthlyClient>> GetClientsStatistics(int? year);
        Task<IEnumerable<AdminCompanyListingModel>> GetAllClients();
        Task<AdminDasboardStatsServiceModel> GetDashboardStats();
    }
}
