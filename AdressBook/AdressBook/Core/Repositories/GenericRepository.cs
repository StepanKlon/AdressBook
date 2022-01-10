using AdressBook.Core.IRepositories;
using AdressBook.Data;
using Microsoft.EntityFrameworkCore;

namespace AdressBook.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext context;
        protected DbSet<T> dbSet;

        public GenericRepository(AppDbContext _context)
        {
            context = _context;
            dbSet = context.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                var result = await dbSet.AddAsync(entity);
                if (result is not null)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                var result = dbSet.Remove(entity);
                if (result is null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<T?> GetById(long id)
        {
            return await dbSet.FindAsync(id);
        }

        public bool Update(T entity)
        {
            try
            {
                var result = dbSet.Update(entity);
                if (result is not null)
                    return true;
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
