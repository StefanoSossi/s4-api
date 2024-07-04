using Microsoft.EntityFrameworkCore;
using s4.Data.Models;
using System.Linq.Expressions;

namespace s4.Data.Repository.Generic
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) { }

        public DatabaseException(string message, Exception innerException) : base(string.Format(message), innerException) { }
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected readonly S4DBContext dbContext;

        public GenericRepository(S4DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                entity.Id = Guid.Empty;
                dbContext.Set<T>().Add(entity);
                await dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new DatabaseException("ERROR: " + e.InnerException.Message);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var asd = dbContext.Set<T>();
            List<T> values = await asd.ToListAsync();
            return values;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            T value = await dbContext.Set<T>().FindAsync(id);
            return value;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> expression)
        {
            return await dbContext.Set<T>().Where(expression).ToListAsync();
        }
    }
}
