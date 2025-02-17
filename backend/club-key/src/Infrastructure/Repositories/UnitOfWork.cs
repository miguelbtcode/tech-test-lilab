namespace Infrastructure.Repositories;

using System.Collections;
using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable _repositories = null!;
    private readonly ClubKeyDbContext _context;
    
    public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
    public async Task CommitTransactionAsync(IDbContextTransaction transaction) => await transaction.CommitAsync();
    public async Task RollbackTransactionAsync(IDbContextTransaction transaction) => await transaction.RollbackAsync();
    
    public UnitOfWork(ClubKeyDbContext context)
    {
        _context = context;
    }

    public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        _repositories ??= new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(RepositoryBase<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IAsyncRepository<TEntity>) _repositories[type]!;
    }

    public async Task<int> Complete()
    {
        try
        {
            return await _context.SaveChangesAsync();   
        }
        catch (Exception e)
        {
            throw new Exception("Error en transaccion", e);
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}