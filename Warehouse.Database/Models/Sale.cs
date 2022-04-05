using System.Collections.Generic;
using System.Linq;
using Warehouse.Database.Helpers;
using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Models
{
    public class Sale : BaseEntity
    {
        [BindedProp(BindedPropType.IEnumerable)]
        public IEnumerable<SaleItem> SaleItems { get; set; } = Enumerable.Empty<SaleItem>();
        [BindedProp]
        public string PartnerId { get; set; }
        [HistoryProp(nameof(PartnerId))]
        public IEnumerable<VersionedProp> PartnerId_history { get; set; }
        [BindedProp]
        public string Description { get; set; }
        [HistoryProp(nameof(Description))]
        public IEnumerable<VersionedProp> Description_history { get; set; }
        [BindedProp]
        public decimal TotalAmount { get; set; }
        [HistoryProp(nameof(TotalAmount))]
        public IEnumerable<VersionedProp> TotalAmount_history { get; set; }
    }
}
