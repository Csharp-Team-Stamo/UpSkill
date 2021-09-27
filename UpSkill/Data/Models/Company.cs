namespace UpSkill.Infrastructure.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Company;

    public class Company
    {
        public Company()
        {
            this.Employees = new HashSet<Employee>();
            this.CourseIncomeInvoices = new HashSet<Invoice<Employee, Company>>();
            this.CoachExpensesInvoices = new HashSet<Invoice<Company, Coach>>();
        }

        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string OwnerId { get; set; }
        public Owner Owner { get; set; }
        
        [Required]
        [StringLength(UIC_Length)]
        public string UIC { get; set; }

        [Required]
        public string Address { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Invoice<Employee, Company>> CourseIncomeInvoices { get; set; }

        public ICollection<Invoice<Company, Coach>> CoachExpensesInvoices { get; set; }
    }
}
