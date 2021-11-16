using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSkill.Infrastructure.Models.Admin
{
    public class AdminCourseRevenueListingModel
    {
        public string CourseName { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
        public decimal Profit => this.Revenue - this.Expenses;
    }
}
