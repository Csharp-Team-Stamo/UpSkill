namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using Infrastructure.Models.Admin;
    using Infrastructure.Models.Company;
    using Contracts;

    public class StatisticsService : IStatisticsService
    {
        private readonly IDeletableEntityRepository<Company> companyRepo;
        private readonly IDeletableEntityRepository<Course> coursesRepo;
        private readonly IDeletableEntityRepository<Coach> coachesRepo;

        public StatisticsService(
            IDeletableEntityRepository<Company> companyRepo,
            IDeletableEntityRepository<Course> coursesRepo,
            IDeletableEntityRepository<Coach> coachesRepo)
        {
            this.companyRepo = companyRepo;
            this.coursesRepo = coursesRepo;
            this.coachesRepo = coachesRepo;
        }

        public async Task<IEnumerable<AdminCompanyListingModel>> GetAllClients()
            => await this.companyRepo
                .All()
                .Select(c => new AdminCompanyListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email
                })
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<IEnumerable<MonthlyClient>>
            GetClientsStatistics(int? year)
        {
            var yearlyClients = await GetCompaniesForYear(year);

            var clientsInMemory = yearlyClients.GroupBy(c => c.CreatedOn.Month);

            var result = new List<MonthlyClient>();

            clientsInMemory.ToList()
                           .ForEach(c => result
                                .Add(new MonthlyClient
                                {
                                    ClientsNum = c.Select(c => c).Count(),
                                    MonthName = ConvertMonthNumToName(c.Key)
                                }));

            //foreach(var client in clientsInMemory)
            //{
            //    var record = new MonthlyClient
            //    {
            //        ClientsNum = client.Select(c => c).Count(),
            //        MonthName = ConvertMonthNumToName(client.Key)
            //    };

            //    result.Add(record);
            //}
                
            return result;
        }

        private async Task<IEnumerable<Company>> GetCompaniesForYear(int? year)
            => await this.companyRepo
                .All()
                .Where(c => c.CreatedOn.Year == year)
                .ToListAsync();

        public async Task<AdminDasboardStatsServiceModel> GetDashboardStats()
            => new AdminDasboardStatsServiceModel
            {
                ClientsNum = await this.GetClientsNum(),
                CoursesNum = await this.GetCoursesNum(),
                CoachesNum = await this.GetCoachesNum(),
                Revenue = 1000
            };

        private async Task<int> GetCoachesNum()
            => await this.coachesRepo
                         .All()
                         .CountAsync();

        private async Task<int> GetCoursesNum()
            => await this.coursesRepo
                         .All()
                         .CountAsync();

        private async Task<int> GetClientsNum()
            => await this.companyRepo
                         .All()
                         .CountAsync() - 1;

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
