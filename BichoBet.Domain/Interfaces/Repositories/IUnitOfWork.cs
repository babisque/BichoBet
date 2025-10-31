namespace BichoBet.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// save all changes made in the context transactionally
    /// </summary>
    /// <returns>The number of state entries written to the database</returns>
    Task<int> SaveChangesAsync();
    
    /// <summary>
    /// check if there are pending changes in the context
    /// </summary>
    /// <returns>True if there are pending changes, false otherwise</returns>
    bool HasPendingChanges();
}