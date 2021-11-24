﻿namespace UpSkill.Data.Models
{
    using System;
    using System.Collections.Generic;
    using UpSkill.Data.Common.Models;

    public class Employee : BaseDeletableModel<string>
    {
        public Employee()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; init; }

        public ApplicationUser User { get; init; }

        public string OwnerId { get; set; }

        public Owner Owner { get; set; }

        public ICollection<CoachEmployee> Coaches { get; set; } = new HashSet<CoachEmployee>();

        public ICollection<EmployeeCourse> Courses { get; set; } = new HashSet<EmployeeCourse>();
        public ICollection<LiveSession> LiveSession { get; set; } = new HashSet<LiveSession>();

        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();

        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}
