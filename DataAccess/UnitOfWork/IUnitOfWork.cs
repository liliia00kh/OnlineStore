using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork<out StoreContext> : IDisposable where StoreContext : DbContext
    {
        ProductRepository ProductRepository { get; }
        StoreContext Context { get; }
        Task CreateTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveAsync();
    }
}
