using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Warehouse.Database.Interfaces;

namespace Warehouse.Database.Models
{
    public class Partner : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string VatNumber { get; set; }
        public string Address { get; set; }

    }
}
