using System.Collections.Generic;
using Warehouse.Database.Helpers;
using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Models
{
    public class SaleItem : BaseEntity
    {
        [BindedProp]
        public string Name { get; set; }
        [HistoryProp(nameof(Name))]
        public IEnumerable<VersionedProp> Name_history { get; set; }
        [BindedProp]
        public decimal Qtty { get; set; }
        [HistoryProp(nameof(Qtty))]
        public IEnumerable<VersionedProp> Qtty_history { get; set; }
        [BindedProp]
        public decimal Price { get; set; }
        [HistoryProp(nameof(Price))]
        public IEnumerable<VersionedProp> Price_history { get; set; }
        [BindedProp]
        public decimal Total { get; set; }
        [HistoryProp(nameof(Total))]
        public IEnumerable<VersionedProp> Total_history { get; set; }
        [BindedProp]
        public string ItemId { get; set; }
        [HistoryProp(nameof(ItemId))]
        public IEnumerable<VersionedProp> ItemId_history { get; set; }
    }
}
