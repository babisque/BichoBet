using System.Linq.Expressions;

namespace BichoBet.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    /// <summary>
    /// Add a new entity to the repository asynchronously
    /// </summary>
    /// <param name="entity">The entity to add</param>
    Task AddAsync(TEntity entity);
    
    /// <summary>
    /// Search for entities by ID asynchronously
    /// </summary>
    /// <param name="id">The ID of the entity to search for</param>
    /// <returns>The entity found or null</returns>
    Task<TEntity?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// returns all entities asynchronously
    /// </summary>
    /// <returns>A readonly collection of all entities</returns>
    Task<IReadOnlyList<TEntity>> GetAllPagedAsync(int page, int pageSize);
    
    /// <summary>
    /// search for entities matching a given predicate asynchronously
    /// </summary>
    /// <returns>A readonly collection of entities matching the predicate</returns>
    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    
    /// <summary>
    /// Set an entity as modified
    /// the real update will occur when SaveChangesAsync is called
    /// </summary>
    /// <param name="entity">The entity to update</param>
    void Update(TEntity entity);
    
    /// <summary>
    /// Remove an entity from the repository
    /// the real deletion will occur when SaveChangesAsync is called
    /// </summary>
    /// <param name="entity">The entity to remove</param>
    void Remove(TEntity entity);
}