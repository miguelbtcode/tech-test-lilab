namespace Application.Contracts.Persistence;

using Microsoft.EntityFrameworkCore.Storage;

public interface IUnitOfWork : IDisposable
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    Task RollbackTransactionAsync(IDbContextTransaction transaction);
    IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> Complete();
}