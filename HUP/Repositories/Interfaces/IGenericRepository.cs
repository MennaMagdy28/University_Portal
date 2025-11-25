using System.Linq.Expressions;
namespace HUP.Repositories.Interfaces
{
    // Generic repository interface for basic CRUD operations
    // T represents the entity type
    // Includes methods for retrieving, adding, updating, soft deleting, and removing entities
    public interface IGenericRepository<T> where T : class
    {
        //CRUD queries + soft delete
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task RemoveAsync(Guid id);
        Task SaveChangesAsync();
    }
}
