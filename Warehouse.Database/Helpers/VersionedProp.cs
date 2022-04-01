using MongoDB.Bson;
using System;
using Warehouse.Database.Interfaces;

namespace Warehouse.Database.Helpers
{
    public class VersionedProp
    {
        public long Version { get; set; }
        public DateTime UpdatedDate { get; set; }
        public BsonDocument Value { get; set; }
    }
}
