using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSkill.Infrastructure.Data.Models
{
    public class Owner : ApplicationUser
    {
        public IQueryable<Employee> Employees { get; set; }
    }
}
