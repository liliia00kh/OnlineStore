using DataAccess.Context;
using DataAccess.Repositories;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork<OnlineStoreDbContext>
    {
        private ProductRepository? _productRepository;
        public UnitOfWork(OnlineStoreDbContext context)
        {
            Context = context;
        }

        public OnlineStoreDbContext Context { get; }
        public IProductRepository ProductRepository
        {
            get { return _productRepository ??= new ProductRepository(Context); }
        }

        public async Task CreateTransactionAsync()
        {
            await Context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await Context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await Context.Database.RollbackTransactionAsync();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
