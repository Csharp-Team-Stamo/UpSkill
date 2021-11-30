namespace UpSkill.Infrastructure.Models.Owner
{
    using System.Collections.Generic;

    public class OwnerInvoiceDetailsModel
    {
        public ICollection<CourseInvoiceModel> MonthlyCoursesToPay { get; set; } =
            new List<CourseInvoiceModel>();

        public ICollection<LiveSessionInvoiceModel> MonthlySessionsToPay { get; set; } =
            new List<LiveSessionInvoiceModel>();

        public decimal TotalMonthlyAmount { get; set; }
    }
}
