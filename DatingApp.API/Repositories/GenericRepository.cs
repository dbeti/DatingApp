using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var dbSet = _context.Set<T>();

            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var dbSet = _context.Set<T>();

            var entity = await dbSet.FindAsync(id);

            dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TResult>> FilterAsync<TResult>(Expression<Func<T, bool>> predicate, 
            Expression<Func<T, TResult>> selector,
            params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>()
                .AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var entities = await query
                .Where(predicate)
                .Select(selector)
                .ToListAsync();

            return entities; 
            
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = _context.Set<T>();

            var entity = await query
                .FindAsync(id);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                var entry = _context.Entry(entity);
                entry.State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return entities;
        }
    }
}