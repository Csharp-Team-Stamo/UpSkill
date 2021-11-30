namespace UpSkill.Infrastructure.Models.Owner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CourseInvoiceModel
    {
        public int CourseId { get; set; }
        public string EmployeeId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
