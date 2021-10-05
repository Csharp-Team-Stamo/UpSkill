namespace UpSkill.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using global::Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Common.Models;
    using Models;

    using static UpSkill.Data.DataConstants.PriceContants;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext()
            : base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Coach> Coaches { get; init; }
        public DbSet<Company> Companies { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Course> Courses { get; init; }
        public DbSet<Employee> Employees { get; init; }
        public DbSet<EmployeeCourse> StudentCourses { get; init; }
        public DbSet<Grade> Grades { get; init; }
        public DbSet<Invoice> Invoices { get; init; }
        public DbSet<InvoiceStatus> InvoiceStatuses { get; init; }
        public DbSet<LiveSession> LiveSessions { get; init; }
        public DbSet<Owner> Owners { get; init; }
        public DbSet<SessionSlot> SessionSlots { get; init; }
        public DbSet<CoachOwner> CoachOwners { get; init; }
        public DbSet<CoachLanguage> CoachLanguages { get; init; }
        public DbSet<CoachEmployee> CoachEmployees { get; init; }
        public DbSet<CoachVote> CoachVotes { get; init; }
        public DbSet<CourseOwner> CourseOwners { get; init; }
        public DbSet<CourseVote> CourseVotes { get; init; }
        public DbSet<Language> Languages { get; init; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=UpSkillTestDB;Trusted_Connection=True;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Coach>()
            .Property(c => c.PricePerSession)
            .HasPrecision(Precision, Scale);

            builder.Entity<Course>()
            .Property(c => c.Price)
            .HasPrecision(Precision, Scale);

            builder.Entity<Invoice>()
            .Property(c => c.Price)
            .HasPrecision(Precision, Scale);

            builder.Entity<LiveSession>()
            .Property(c => c.Price)
            .HasPrecision(Precision, Scale);

            base.OnModelCreating(builder);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
         where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditInfo)
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
