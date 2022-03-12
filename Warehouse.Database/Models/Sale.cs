using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Database.Interfaces;

namespace Warehouse.Database.Models
{
    public class Sale : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public IEnumerable<string> ItemsId { get; set; } = Enumerable.Empty<string>();
        public string PartnerId { get; set; }
        public string Description { get; set; }
    }
}
