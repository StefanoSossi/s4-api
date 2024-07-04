using s4.Data.Models;
using System.Linq.Expressions;

namespace s4.Data.Repository.Generic
{
    internal interface IGenericRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);

        Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> expression);
    }
}
