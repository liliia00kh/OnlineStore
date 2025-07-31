using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        private readonly DbSet<Product> _dbSet;
        public ProductRepository(OnlineStoreDbContext context) : base(context)
        {
            _dbSet = context.Set<Product>();
        }
    }
}
