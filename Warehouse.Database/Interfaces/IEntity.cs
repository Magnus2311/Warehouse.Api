using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using Warehouse.Database.Helpers;
using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Interfaces
{
    public interface IEntity
    {
        public ObjectId Id { get; set; }
        [BsonDefaultValue(false)]
        [BindedProp]
        public bool IsDeleted { get; set; }
        [HistoryProp(nameof(IsDeleted))]
        public IEnumerable<VersionedProp> IsDeleted_history { get; set; }
        public long Version { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public ObjectId UserId { get; set; }
    }
}
