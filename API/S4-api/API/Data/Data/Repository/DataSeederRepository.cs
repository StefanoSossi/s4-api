
using s4.Data.DataInitialization;
using s4.Data.Models;
using s4.Data.Repository.Generic;
using s4.Data.Repository.Interfaces;

namespace s4.Data.Repository
{
    public  class DataSeederRepository(S4DBContext dbContext) : IDataSeederRepository
    {
        protected readonly S4DBContext dbContext = dbContext;

        public Task<string> CreateAsync()
        {
            try
            {
                if (!dbContext.Set<Student>().Any())
                {
                    SeedData(dbContext, DataSeeder.SeedClass());
                    SeedData(dbContext, DataSeeder.SeedStudent());
                    SeedData(dbContext, DataSeeder.SeedStudentClass());
                    return Task.FromResult("Data Seeding complete");
                }
                throw new DatabaseException("ERROR: The Database has already been Seeded");
            }
            catch (Exception e)
            {
                throw new DatabaseException("ERROR: " + e);
            }
        }

        private static void SeedData<T>(S4DBContext dbContext, IEnumerable<T> seedDataEntities)
        where T : class
        {
            try
            {
                IEnumerable<T> entities = seedDataEntities;
                foreach (T entity in entities)
                {
                    dbContext.Set<T>().Add(entity);
                }
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DatabaseException($"ERROR in {typeof(T).Name} Seeding: {e.InnerException.Message}");
            }
        }
    }
}
