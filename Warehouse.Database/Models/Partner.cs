using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Warehouse.Database.Interfaces;

namespace Warehouse.Database.Models
{
    public class Partner : IEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string VatNumber { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
    }
}
