namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Admin;
    using UpSkill.Infrastructure.Models.Company;
    using UpSkill.Services.Data.Contracts;

    public class StatisticsService : IStatisticsService
    {
        private readonly IDeletableEntityRepository<Company> companyRepo;

        public StatisticsService(IDeletableEntityRepository<Company> companyRepo)
        {
            this.companyRepo = companyRepo;
        }

        public async Task<IEnumerable<AdminCompanyListingModel>> GetAllClients()
        {
            var allClients = await this.companyRepo
                .All()
                .Select(c => new AdminCompanyListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email
                })
                .OrderBy(c => c.Name)
                .ToListAsync();

            return allClients;
        }

        public async Task<IEnumerable<MonthlyClient>>
            GetClientsStatistics(int? year)
        {
            var yearlyClients = await this.companyRepo
                .All()
                .Where(c => c.CreatedOn.Year == year)
                .GroupBy(c => c.CreatedOn.Month)
                .ToListAsync();

            var res = yearlyClients
                .Select(c => new MonthlyClient
                {
                    MonthName = ConvertMonthNumToName(c.Key),
                    ClientsNum = c.Select(c => c).Count()
                });
                
            return res;
        }

        private string ConvertMonthNumToName(int monthAsNum)
        {
            switch(monthAsNum)
            {
                case 1: return "Jan";
                case 2: return "Feb";
                case 3: return "Mar";
                case 4: return "Apr";
                case 5: return "May";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Aug";
                case 9: return "Sep";
                case 10: return "Oct";
                case 11: return "Nov";
                case 12: return "Dec";
                default: return null;
            }
        }
    }
}
