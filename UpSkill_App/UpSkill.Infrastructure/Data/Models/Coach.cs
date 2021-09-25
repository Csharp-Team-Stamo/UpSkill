namespace UpSkill.Infrastructure.Data.Models
{
    using System.Linq;

    public class Coach : ApplicationUser
    {
        public IQueryable<Course> Courses { get; set; }
    }
}
