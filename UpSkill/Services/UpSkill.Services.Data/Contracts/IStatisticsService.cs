namespace UpSkill.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Admin;
    using UpSkill.Infrastructure.Models.Company;

    public interface IStatisticsService
    {
        Task<IEnumerable<MonthlyClient>> GetClientsStatistics(int? year);
        Task<IEnumerable<AdminCompanyListingModel>> GetAllClients();
    }
}
