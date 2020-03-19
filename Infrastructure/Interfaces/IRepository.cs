using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Infrastructure.Interfaces
{
        public interface IRepository<T> where T : BaseEntity
        {
            T GetById(int id);
            void Add(T entity);
            void Delete(T entity);
            void Update(T entity);
            IEnumerable<T> GetAll();
            IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
            Task<T> GetByIdAsync(int id);
        }
}
