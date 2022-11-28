using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.Database;
using Trading.Interface.Interface;

namespace Trading.Infrastructure.Repository
{
   public class UnitOfWork : IUnitOfWork
    {
		private readonly TradingContext _dbContext;
		private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

		public Dictionary<Type, object> Repositories
		{
			get { return _repositories; }
			set { Repositories = value; }
		}

		public UnitOfWork(TradingContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IBaseRepository<T> Repository<T>() where T : class
		{
			if (Repositories.Keys.Contains(typeof(T)))
			{
				return Repositories[typeof(T)] as IBaseRepository<T>;
			}

			IBaseRepository<T> repo = new BaseRepository<T>(_dbContext);
			Repositories.Add(typeof(T), repo);
			return repo;
		}

		public async Task<int> Commit()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public void Rollback()
		{
			_dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
		}
	}
}
