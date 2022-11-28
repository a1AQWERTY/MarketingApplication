using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Interface.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        #region Get

        IQueryable<T> GetByWhere(Expression<Func<T, bool>> predicate);

        IEnumerable<T> ListAll();

        #endregion

        #region Add
        T Add(T modelToAdd);
        Task<T> AddAsync(T modelToAdd);
        void AddRange(IList<T> modelsToAdd);
        Task AddRangeAsync(IList<T> modelsToAdd);
        #endregion

        #region Update
        void Update(T modelToUpdate);
        void UpdateRange(IList<T> modelsToUpdate);
        Task<T> UpdateAsync(T modelToUpdate);
        Task UpdateRangeAsync(IList<T> modelsToUpdate);


        #endregion

        #region Delete
        void Delete(T modelToDelete);
        void DeleteRange(IList<T> modelsToDelete);
        Task<T> DeleteAsync(T modelToDelete);
        Task DeleteRangeAsync(IList<T> modelsToDelete);
        #endregion

        #region Global
        Task<int> SaveChangesAsync();
        int SaveChanges();
        bool Exist(Expression<Func<object, bool>> predicate);
        #endregion
    }
}
