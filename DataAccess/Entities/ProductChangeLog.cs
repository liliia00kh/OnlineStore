using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Entities
{
    public class ProductChangeLog
    {
        [BsonId] // Primary key у MongoDB
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ChangedBy { get; set; } = string.Empty; // UserName або Admin
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public string Field { get; set; } = string.Empty;  // Напр. "Price"
        public string OldValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
    }
}
