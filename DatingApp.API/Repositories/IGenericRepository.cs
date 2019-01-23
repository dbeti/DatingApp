using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DatingApp.API.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities);

        Task DeleteAsync(int id);

        Task<IEnumerable<TResult>> FilterAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, params Expression<Func<T, object>>[] includes);
    }
}