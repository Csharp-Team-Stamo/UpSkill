namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Company;

    using UpSkill.Data.Common.Models;
    using UpSkill.Infrastructure.Common.Attributes;

    public class Company : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(NameMaxLen)]
        public string Name { get; set; }

        [IsEmail]
        public string Email { get; set; }

        [MaxLength(UIC_Length)]
        public string UIC { get; set; }

        public string Address { get; set; }

        public ICollection<Owner> Owners { get; set; } = 
            new HashSet<Owner>();

        public ICollection<Employee> Employees { get; set; } = 
            new HashSet<Employee>();

        public ICollection<Invoice> Invoices { get; set; } = 
            new HashSet<Invoice>();
    }
}
