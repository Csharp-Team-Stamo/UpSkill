using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpSkill.Infrastructure.Models.Admin;
using UpSkill.Infrastructure.Models.Company;
using UpSkill.Services.Data.Contracts;

namespace UpSkill.Api.Areas.Admin.Controllers
{
    public class StatisticsController : AdminController
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet("ClientsByMonth/{year}")]
        public async Task<ActionResult<IEnumerable<MonthlyClient>>> ClientsByMonth(int? year)
        {
            if(year == null)
            {
                year = DateTime.UtcNow.Year;
            }

            var result = await this.statisticsService.GetClientsStatistics(year);

            if(result.Any() == false)
            {
                return NotFound();
            }

            return new List<MonthlyClient>(result);
        }

        [HttpGet("GetAllClients")]
        public async Task<ActionResult<IEnumerable<AdminCompanyListingModel>>> GetAllClients()
        {
            var allClients = await this.statisticsService.GetAllClients();

            if(allClients.Any() == false)
            {
                return NotFound();
            }

            return new List<AdminCompanyListingModel>(allClients);
        }

        [HttpGet("GetDashboardStats")]
        public async Task<ActionResult<AdminDasboardStatsServiceModel>> 
            GetDashboardStats()
        {
            var stats = await this.statisticsService.GetDashboardStats();

            return stats;
        }
    }
}
