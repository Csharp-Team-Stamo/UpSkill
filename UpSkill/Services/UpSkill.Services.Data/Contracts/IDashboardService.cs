namespace UpSkill.Services.Data.Contracts
{
    using Infrastructure.Models.Dashboard;

    public interface IDashboardService
    {
        EmployeeDashboardModel GetEmployeeDashboardInfoById(string employeeId);
    }
}
