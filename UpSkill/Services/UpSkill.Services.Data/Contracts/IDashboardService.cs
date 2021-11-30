namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Dashboard;

    public interface IDashboardService
    {
        Task<EmployeeDashboardModel> GetEmployeeDashboardInfoByIdAsync(string employeeId);
    }
}
