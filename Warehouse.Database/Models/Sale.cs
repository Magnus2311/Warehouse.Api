using System.Collections.Generic;
using System.Linq;
using Warehouse.Database.Helpers;
using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Models
{
    public class Sale : BaseEntity
    {
        [BindedProp]
        public IEnumerable<SaleItem> SaleItems { get; set; } = Enumerable.Empty<SaleItem>();
        public IEnumerable<VersionedProp> SaleItems_history { get; set; }
        [BindedProp]
        public string PartnerId { get; set; }
        public IEnumerable<VersionedProp> PartnerId_history { get; set; }
        [BindedProp]
        public string Description { get; set; }
        public IEnumerable<VersionedProp> Description_history { get; set; }
        [BindedProp]
        public decimal TotalAmount { get; set; }
        public IEnumerable<VersionedProp> TotalAmount_history { get; set; }
    }
}
