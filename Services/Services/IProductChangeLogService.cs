using DataAccess.Entities;

namespace Services.Services
{
    public interface IProductChangeLogService
    {
        Task AddLogAsync(ProductChangeLog log);
        Task<List<ProductChangeLog>> GetLogsAsync(int productId);
    }
}
