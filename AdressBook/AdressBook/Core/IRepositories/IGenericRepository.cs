namespace AdressBook.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>?> All();
        Task<T?> GetById(long id);
        Task<bool> Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
    }
}
