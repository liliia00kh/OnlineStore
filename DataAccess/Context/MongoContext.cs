using DataAccess.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataAccess.Context
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string ProductChangesCollection { get; set; } = string.Empty;
    }
    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoSettings _settings;

        public MongoContext(IOptions<MongoSettings> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.Database);
        }

        public IMongoCollection<ProductChangeLog> ProductChangeLogs =>
            _database.GetCollection<ProductChangeLog>(_settings.ProductChangesCollection);
    }
}
