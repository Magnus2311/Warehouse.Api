using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Warehouse.Database.Models
{
    public class SaleItem
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public decimal Qtty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string ItemId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
