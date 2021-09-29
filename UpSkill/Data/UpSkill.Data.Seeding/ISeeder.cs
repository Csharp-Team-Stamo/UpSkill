namespace UpSkill.Data.Seeding
{
	using System;
	using System.Threading.Tasks;

	using UpSkill.Data;

	public interface ISeeder
	{
		Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
	}
}
