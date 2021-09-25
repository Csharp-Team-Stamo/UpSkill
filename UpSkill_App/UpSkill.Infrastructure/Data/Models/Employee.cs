using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSkill.Infrastructure.Data.Models
{
    public class Employee : ApplicationUser
    {
        public IQueryable<Course> Courses { get; set; }
    }
}
