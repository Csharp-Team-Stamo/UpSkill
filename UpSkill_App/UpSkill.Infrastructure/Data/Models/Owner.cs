namespace UpSkill.Infrastructure.Data.Models
{
    using System.Collections.Generic;

    public class Owner : ApplicationUser
    {
        public Owner()
        {
            this.Employees = new HashSet<Employee>();
        }

        public ICollection<Employee> Employees { get; set; }
    }
}
