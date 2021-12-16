namespace UpSkill.Infrastructure.Models.Owner
{
    using System;

    public class CourseInvoiceModel
    {
        public int CourseId { get; set; }
        public string EmployeeId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
