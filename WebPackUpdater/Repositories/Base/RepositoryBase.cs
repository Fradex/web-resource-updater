using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebPackUpdater.Repositories.Base
{
	public class RepositoryBase<TContext, TEntity> where TContext : DbContext where TEntity : class
	{
		protected TContext Context;

		public RepositoryBase(TContext context)
		{
			Context = context;
		}

		public IQueryable<TEntity> GetAll()
		{
			return Context.Query<TEntity>();
		}
	}
}