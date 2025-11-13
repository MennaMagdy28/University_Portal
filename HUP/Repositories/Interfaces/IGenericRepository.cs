using System.Linq.Expressions;
namespace HUP.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void SoftDelete(Guid id);
        void Remove(Guid id);
        Task SaveChangesAsync();
    }
}
