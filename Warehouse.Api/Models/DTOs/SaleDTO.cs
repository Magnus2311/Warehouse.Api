using System;
using System.Collections.Generic;
using System.Linq;

namespace Warehouse.Api.Models.DTOs
{
    public class SaleDTO
    {
        public string Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public IEnumerable<string> ItemsId { get; set; } = Enumerable.Empty<string>();
        public string PartnerId { get; set; }
        public string Description { get; set; }
    }
}
