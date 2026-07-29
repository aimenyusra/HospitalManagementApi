namespace Hospital.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
