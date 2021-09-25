namespace UpSkill.Infrastructure.Data
{
    using UpSkill.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Administrator> Administrators { get; init; }

        public DbSet<Owner> Owners { get; init; }

        public DbSet<Employee> Employees { get; init; }

        public DbSet<Coach> Coaches { get; init; }

        public DbSet<Course> Courses { get; init; }

        public DbSet<Grade> Grades { get; init; }
    }
}
