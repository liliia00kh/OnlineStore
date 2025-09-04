using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IProductChangeLogRepository
    {
        Task AddLogAsync(ProductChangeLog log);
        Task<List<ProductChangeLog>> GetLogsAsync(int productId);
    }
}
