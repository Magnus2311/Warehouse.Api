using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Warehouse.Database.Interfaces
{
    public interface IEntity
    {
        public ObjectId Id { get; set; }
        [BsonDefaultValue(false)]
        public bool IsDeleted { get; set; }
        public long Version { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
