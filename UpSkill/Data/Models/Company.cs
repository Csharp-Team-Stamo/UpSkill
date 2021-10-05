namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using UpSkill.Data.Common.Models;

    using static UpSkill.Data.DataConstants.Company;

    public class Company : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        // [StringLength(UIC_Length)]
        public string UIC { get; set; }

        public string Address { get; set; }

        public ICollection<Owner> Owners { get; set; } = new HashSet<Owner>();

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}
