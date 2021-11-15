namespace UpSkill.Infrastructure.Models.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AdminTotalRevenueModel
    {
        public IEnumerable<AdminCourseRevenueListingModel> CoursesInfo { get; set; }
        public decimal TotalProfit => CalculateTotalProfit();

        private decimal CalculateTotalProfit()
            => this.CoursesInfo.Sum(c => c.Profit);
    }
}
