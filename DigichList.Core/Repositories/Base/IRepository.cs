using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories.Base
{
    public interface IRepository<T, K>
    {
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        public Task<T> GetByIdAsync(K id);
        public Task AddAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task UpdateAsync(T entity);

    }
}
