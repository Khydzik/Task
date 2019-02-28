using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LearningProject.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        public Repository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>();
        }

        public IEnumerable<T> Insert(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);

            _dbContext.SaveChanges();

            return entities;
        }

        public async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);

            await _dbContext.SaveChangesAsync();

            return entities;
        }

        public T Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public T Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);

            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public IEnumerable<T> Update(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);

            _dbContext.SaveChanges();

            return entities;
        }

        public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);

            await _dbContext.SaveChangesAsync();

            return entities;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);

            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}