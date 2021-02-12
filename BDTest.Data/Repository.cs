using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BDTest.Core.Models;

namespace BDTest.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext context;
        private DbSet<T> set;

        public Repository(DatabaseContext context)
        {
            this.context = context;
            set = context.Set<T>();
        }

        public IQueryable<T> GetQuery()
        {
            return set.AsQueryable();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> where = null)
        {
            var query = set.AsQueryable();
            
            if (where != null)
                query = query.Where(where);
                
            return query;
        }

        public IAsyncEnumerable<T> GetAllAsync(Expression<Func<T, bool>> where = null)
        {
            return GetAllAsyncIncluding<int>(where, null);
        }

        public IAsyncEnumerable<T> GetAllAsyncIncluding<TProperty>(Expression<Func<T, bool>> where = null, Expression<Func<T, TProperty>> navigationPropertyPath = null)
        {
            var query = set.AsQueryable();
            
            if (where != null)
                query = query.Where(where);
            
            if (navigationPropertyPath != null)
                query = query.Include(navigationPropertyPath);
                
            return query.AsAsyncEnumerable();
        }

        public Task<T> GetByIdAsync(long id)
        {
           return GetByIdAsyncIncluding<Int32>(id, null);
        }

        public Task<T> GetByIdAsyncIncluding<TProperty>(long id, Expression<Func<T, TProperty>> navigationPropertyPath = null)
        {
            var query = set.AsQueryable();

            if (navigationPropertyPath != null)
                query = query.Include(navigationPropertyPath);

           return query.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task InsertAsync(T entity)
        {
            await set.AddAsync(entity);
            context.SaveChanges();
        }

        public async Task BulkInsertAsync(IEnumerable<T> entities)
        {
            await BulkInsertAsync(entities.ToArray());
        }

        public async Task BulkInsertAsync(T[] entities)
        {
            await set.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            return context.SaveChangesAsync();
        }
        public void Delete(long id)
        {
            T entity = set.SingleOrDefault(s => s.Id == id);
            set.Remove(entity);
            context.SaveChanges();
        }

        public Task BulkDeleteAsync(IEnumerable<T> entities)
        {
            return BulkDeleteAsync(entities.ToArray());
        }

        public Task BulkDeleteAsync(T[] entities)
        {
            set.RemoveRange(entities);
            return context.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public Task LoadReference<TProperty>(T entity, Expression<Func<T, TProperty>> propertyExpression) where TProperty : class
        {
            return context.Entry(entity).Reference(propertyExpression).LoadAsync();
        }

        public Task ReloadAsync(T entity)
        {
            return context.Entry(entity).ReloadAsync();
        }
    }
}