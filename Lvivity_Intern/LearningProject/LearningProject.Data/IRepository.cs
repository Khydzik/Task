using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LearningProject.Data
{
    public interface IRepository<T> where T : class
    {
        T Delete(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Insert(IEnumerable<T> entities);
        T Insert(T entity);
        Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities);
        Task<T> InsertAsync(T entity);
        IQueryable<T> Query();
        IEnumerable<T> Update(IEnumerable<T> entities);
        T Update(T entity);
        Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
    }
}
