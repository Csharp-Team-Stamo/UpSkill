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
                .ToListAsync();

            var clientsInMemory = yearlyClients
                .GroupBy(c => c.CreatedOn.Month);

            var result = new List<MonthlyClient>();

            foreach(var client in clientsInMemory)
            {
                var record = new MonthlyClient
                {
                    ClientsNum = client.Select(c => c).Count(),
                    MonthName = ConvertMonthNumToName(client.Key)
                };

                result.Add(record);
            }
                
            return result;
        }

        public async Task<AdminDasboardStatsServiceModel> GetDashboardStats()
        {
            var statsModel = new AdminDasboardStatsServiceModel();

            statsModel.ClientsNum = await this.GetClientsNum();
            statsModel.CoursesNum = await this.GetCoursesNum();
            statsModel.CoachesNum = await this.GetCoachesNum();
            statsModel.Revenue = 1000;

            return statsModel;
        }

        private async Task<int> GetCoachesNum()
        {
            var coachesNum = this.coachesRepo.All().Count();

            await Task.Delay(0);

            return coachesNum;
        }

        private async Task<int> GetCoursesNum()
        {
            var allCoursesCount = this.coursesRepo
                .All()
                .Count();

            await Task.Delay(0);

            return allCoursesCount;
        }

        private async Task<int> GetClientsNum()
        {
            var res = this.companyRepo
                .All()
                .Count();

            await Task.Delay(0);

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
