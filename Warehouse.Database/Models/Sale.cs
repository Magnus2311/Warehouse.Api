using System.Collections.Generic;
using System.Linq;

namespace Warehouse.Database.Models
{
    public class Sale : BaseEntity
    {
        public IEnumerable<SaleItem> SaleItems { get; set; } = Enumerable.Empty<SaleItem>();
        public string PartnerId { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
