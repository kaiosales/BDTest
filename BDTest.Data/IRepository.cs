using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using BDTest.Core.Models;

namespace BDTest.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetQuery();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> where = null);
        IAsyncEnumerable<T> GetAllAsync(Expression<Func<T, bool>> where = null);
        IAsyncEnumerable<T> GetAllAsyncIncluding<TProperty>(Expression<Func<T, bool>> where = null, Expression<Func<T, TProperty>> navigationPropertyPath = null);
        Task<T> GetByIdAsync(long id);
        Task<T> GetByIdAsyncIncluding<TProperty>(long id, Expression<Func<T, TProperty>> navigationPropertyPath = null);
        Task InsertAsync(T entity);
        Task BulkInsertAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        void Delete(long id);
        Task BulkDeleteAsync(IEnumerable<T> entities);
        Task BulkDeleteAsync(T[] entities);
        Task SaveChangesAsync();
        Task LoadReference<TProperty>(T entity, Expression<Func<T, TProperty>> propertyExpression) where TProperty : class;
        Task ReloadAsync(T entity);
    }
}