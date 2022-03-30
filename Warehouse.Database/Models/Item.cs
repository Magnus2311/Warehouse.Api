using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Warehouse.Database.Interfaces;

namespace Warehouse.Database.Models
{
    public class Item : IEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public double BasePrice { get; set; }
        public double SellPrice { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsDeleted { get; set; }
    }
}
