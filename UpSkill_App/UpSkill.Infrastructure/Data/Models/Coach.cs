namespace UpSkill.Infrastructure.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Coach : ApplicationUser
    {
        public decimal PricePerSession { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<LiveSession> LiveSessions { get; set; }

        public ICollection<DateTime> DatesAvailable { get; set; }

        public ICollection<Invoice<Company, Coach>> CourseInvoices { get; set; }

        public ICollection<Invoice<Employee, Coach>> LiveSessionInvoices { get; set; }
    }
}
