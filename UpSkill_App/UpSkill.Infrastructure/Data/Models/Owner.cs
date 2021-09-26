namespace UpSkill.Infrastructure.Data.Models
{
    using System.Collections.Generic;

    public class Owner : ApplicationUser
    {
        public ICollection<Employee> Employees { get; set; }
    }
}
