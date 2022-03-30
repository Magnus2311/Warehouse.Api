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
        public ObjectId Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public IEnumerable<SaleItem> SaleItems { get; set; } = Enumerable.Empty<SaleItem>();
        public string PartnerId { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsDeleted { get; set; }
    }
}
