using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Trading.Data.Database;
using Trading.Interface.Interface;

namespace Trading.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
       protected readonly TradingContext _dbContext;

        private DbSet<T> _dbSet;
        

        public BaseRepository(TradingContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        #region Get
        public IQueryable<T> GetByWhere(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsQueryable<T>().Where(predicate);
        }

        public IEnumerable<T> ListAll()
        {
            return _dbSet.AsQueryable<T>();
        }
        #endregion

        #region Add
        public T Add(T modelToAdd)
        {
            _dbContext.Set<T>().Add(modelToAdd);
            return modelToAdd;
        }
        public async Task<T> AddAsync(T modelToAdd)
        {
            await _dbContext.Set<T>().AddAsync(modelToAdd);
            return modelToAdd;
        }
        public void AddRange(IList<T> modelsToAdd)
        {
            _dbContext.Set<T>().AddRange(modelsToAdd);
        }
        public async Task AddRangeAsync(IList<T> modelsToAdd)
        {
            await _dbContext.Set<T>().AddRangeAsync(modelsToAdd);
        }
        #endregion

        #region Update
        public void Update(T modelToUpdate)
        {
            _dbContext.Set<T>().Update(modelToUpdate).State = EntityState.Modified;
        }
        public void UpdateRange(IList<T> modelsToUpdate)
        {
            _dbContext.Set<T>().UpdateRange(modelsToUpdate);
        }

        public async Task<T> UpdateAsync(T modelToUpdate)
        {
            _dbContext.Set<T>().Update(modelToUpdate).State = EntityState.Modified;
            return modelToUpdate;
        }
        public async Task UpdateRangeAsync(IList<T> modelsToUpdate)
        {
            _dbContext.Set<T>().UpdateRange(modelsToUpdate);
            
        }
        #endregion

        #region Delete
        public void Delete(T modelToDelete)
        {
            _dbContext.Set<T>().Remove(modelToDelete);
        }
        public void DeleteRange(IList<T> modelsToDelete)
        {
            _dbContext.Set<T>().RemoveRange(modelsToDelete);

        }

        public async Task<T> DeleteAsync(T modelToDelete)
        {
            _dbContext.Set<T>().Remove(modelToDelete);
            return modelToDelete;
        }
        public async Task DeleteRangeAsync(IList<T> modelsToDelete)
        {
            _dbContext.Set<T>().RemoveRange(modelsToDelete);
        }
        #endregion

        #region Global
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public bool Exist(Expression<Func<object, bool>> predicate)
        {
            return _dbSet.AsQueryable().Any(predicate);
        }
        #endregion


    }
}
