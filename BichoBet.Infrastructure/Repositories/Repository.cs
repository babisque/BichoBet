using System.Linq.Expressions;
using BichoBet.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BichoBet.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    /// <summary>
    /// Add the entity to ChangeTracker's EF
    /// The SQL query will be executed when SaveChangesAsync is called
    /// </summary>
    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    /// <summary>
    /// Search for an entity by its primary key
    /// it's the fastest way to search for an entity
    /// </summary>
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// find all table entities
    /// </summary>
    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    /// <summary>
    /// search entities by a filter expression LINQ
    /// </summary>
    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate)
            .ToListAsync();
    }
    
    /// <summary>
    /// set the entity as modified in ChangeTracker's EF
    /// The SQL query will be executed when SaveChangesAsync is called
    /// </summary>
    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity); 
        _context.Entry(entity).State = EntityState.Modified;
    }

    /// <summary>
    /// set the entity as deleted in ChangeTracker's EF
    /// The SQL query will be executed when SaveChangesAsync is called
    /// </summary>
    public void Remove(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);

        _dbSet.Remove(entity);
    }

    #region Disposable Support
    private bool _disposed = false;
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();

        _disposed = true;
    }

    /// <summary>
    /// release the dbcontext
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}