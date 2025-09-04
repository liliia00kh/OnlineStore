using DataAccess.Context;
using DataAccess.Entities;
using MongoDB.Driver;

namespace DataAccess.Repositories
{
    public class ProductChangeLogRepository : IProductChangeLogRepository
    {
        private readonly MongoContext _context;

        public ProductChangeLogRepository(MongoContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(ProductChangeLog log)
        {
            await _context.ProductChangeLogs.InsertOneAsync(log);
        }

        public async Task<List<ProductChangeLog>> GetLogsAsync(int productId)
        {
            return await _context.ProductChangeLogs
                .Find(l => l.ProductId == productId)
                .SortByDescending(l => l.ChangedAt)
                .ToListAsync();
        }
    }
}
