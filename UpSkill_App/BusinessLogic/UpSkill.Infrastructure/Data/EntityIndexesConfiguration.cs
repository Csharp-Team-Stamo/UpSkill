namespace UpSkill.Infrastructure.Data
{
	using System.Linq;

	using UpSkill.Infrastructure.Data.Common.Models;

	using Microsoft.EntityFrameworkCore;

	internal static class EntityIndexesConfiguration
	{
		public static void Configure(ModelBuilder modelBuilder)
		{
			// IDeletableEntity.IsDeleted index
			var deletableEntityTypes = modelBuilder.Model
				.GetEntityTypes()
				.Where(et => et.ClrType != null)
				.Where(et => typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));

			foreach (var deletableEntityType in deletableEntityTypes)
			{
				modelBuilder.Entity(deletableEntityType.ClrType)
				.HasIndex(nameof(IDeletableEntity.IsDeleted));
			}
		}
	}
}
