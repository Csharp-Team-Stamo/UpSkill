namespace UpSkill.Infrastructure.Models.Owner
{
    using System;

    public class LiveSessionInvoiceModel
    {
        public string SessionId { get; set; }
        public string EmployeeId { get; set; }
        public string CoachName { get; set; }
        public decimal Price { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
