namespace UpSkill.Infrastructure.Data.Common.Repositories
{
	using System.Linq;

	using UpSkill.Infrastructure.Data.Common.Models;

	public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
		where TEntity : class, IDeletableEntity
	{
		IQueryable<TEntity> AllWithDeleted();

		IQueryable<TEntity> AllAsNoTrackingWithDeleted();

		void HardDelete(TEntity entity);

		void Undelete(TEntity entity);
	}
}
