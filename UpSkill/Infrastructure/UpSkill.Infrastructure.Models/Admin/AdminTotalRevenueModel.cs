namespace UpSkill.Infrastructure.Models.Admin
{
    using System.Collections.Generic;
    using System.Linq;

    public class AdminTotalRevenueModel
    {
        public IEnumerable<AdminCourseRevenueListingModel> CoursesInfo { get; set; }
        public decimal TotalProfit => CalculateTotalProfit();

        private decimal CalculateTotalProfit()
            => this.CoursesInfo.Sum(c => c.Profit);
    }
}
