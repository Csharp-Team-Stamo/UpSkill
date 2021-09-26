namespace UpSkill.Infrastructure.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Coach : ApplicationUser
    {
        public Coach()
        {
            this.Courses = new HashSet<Course>();
            this.LiveSessions = new HashSet<LiveSession>();
            this.DatesAvailable = new HashSet<DateTime>();
            this.CourseInvoices = new HashSet<Invoice<Company, Coach>>();
            this.LiveSessionInvoices = new HashSet<Invoice<Employee, Coach>>();
        }

        public decimal PricePerSession { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<LiveSession> LiveSessions { get; set; }

        public ICollection<DateTime> DatesAvailable { get; set; }

        public ICollection<Invoice<Company, Coach>> CourseInvoices { get; set; }

        public ICollection<Invoice<Employee, Coach>> LiveSessionInvoices { get; set; }
    }
}
