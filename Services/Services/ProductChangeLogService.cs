using DataAccess.Entities;
using DataAccess.Repositories;

namespace Services.Services
{
    public class ProductChangeLogService : IProductChangeLogService
    {
        private readonly IProductChangeLogRepository _logRepository;
        public ProductChangeLogService(IProductChangeLogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task AddLogAsync(ProductChangeLog log)
        {
            await _logRepository.AddLogAsync(log);
        }

        public async Task<List<ProductChangeLog>> GetLogsAsync(int productId)
        {
            return await _logRepository.GetLogsAsync(productId);
        }
    }
}
