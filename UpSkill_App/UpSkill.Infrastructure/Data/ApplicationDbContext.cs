namespace UpSkill.Infrastructure.Data
{
	using UpSkill.Infrastructure.Data.Models;
	using UpSkill.Infrastructure.Data.Common.Models;

	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;

	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;
	using System;
	using System.Linq;

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
			typeof(ApplicationDbContext).GetMethod(
				nameof(SetIsDeletedQueryFilter),
				BindingFlags.NonPublic | BindingFlags.Static);

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
		// Or maybe I should do a new DbSet for each invoice relation:
		// Employee pays company for course
		// Company pays coach for course
		// Employee pays coach for live session
		//public DbSet<Invoice<???, ???>> Invoices { get; init; }

		public DbSet<LiveSession> LiveSessions { get; init; }

		public DbSet<Owner> Owners { get; init; }

		public DbSet<StudentCourse> StudentCourses { get; init; }

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
