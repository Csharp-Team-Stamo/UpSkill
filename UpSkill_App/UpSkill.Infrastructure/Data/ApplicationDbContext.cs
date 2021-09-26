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

        public DbSet<ApplicationUser> ApplicationUsers { get; init; }

        public DbSet<Coach> Coaches { get; init; }

        public DbSet<Company> Companies { get; init; }

        public DbSet<Course> Courses { get; init; }

        public DbSet<Employee> Employees { get; init; }

        public DbSet<Grade> Grades { get; init; }

        // TODO:
        // Can't remember how to make a DBSet of a class that contains T properties
        //public DbSet<Invoice<???, ???>> Invoices { get; init; }

        public DbSet<LiveSession> LiveSessions { get; init; }

        public DbSet<Owner> Owners { get; init; }

        public DbSet<StudentCourse> StudentCourses { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // <Coach relations>
            builder
                .Entity<Course>()
                .HasOne(c => c.Coach)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CoachId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<LiveSession>()
                .HasOne(ls => ls.Coach)
                .WithMany(c => c.LiveSessions)
                .HasForeignKey(ls => ls.CoachId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Invoice<Company, Coach>>()
                .HasOne(i => i.Seller)
                .WithMany(c => c.CourseInvoices)
                .HasForeignKey(i => i.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Invoice<Employee, Coach>>()
                .HasOne(i => i.Seller)
                .WithMany(c => c.LiveSessionInvoices)
                .HasForeignKey(i => i.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            // </Coach relations>



            // <Company relations>

            builder
                .Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Invoice<Employee, Company>>()
                .HasOne(i => i.Seller)
                .WithMany(c => c.CourseIncomeInvoices)
                .HasForeignKey(i => i.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Invoice<Company, Coach>>()
                .HasOne(i => i.Buyer)
                .WithMany(c => c.CoachExpensesInvoices)
                .HasForeignKey(i => i.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // </Company relations>



            // <Employee relations>

            builder
                .Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(e => e.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Invoice<Employee, Company>>()
                .HasOne(i => i.Buyer)
                .WithMany(e => e.CourseInvoices)
                .HasForeignKey(i => i.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Invoice<Employee, Coach>>()
                .HasOne(i => i.Buyer)
                .WithMany(e => e.LiveSessionInvoices)
                .HasForeignKey(i => i.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // </Employee relations>



            // <StudentCourse relations>

            builder
                .Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder
                .Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(e => e.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // </StudentCourse relations>

            base.OnModelCreating(builder);
        }
    }
}
