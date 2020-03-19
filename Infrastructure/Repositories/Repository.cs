using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        internal DbContext _context;
        internal DbSet<T> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
