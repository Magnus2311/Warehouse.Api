using System.Collections.Generic;
using Warehouse.Database.Helpers;
using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Models
{
    public class Item : BaseEntity
    {
        [BindedProp]
        public string Name { get; set; }
        [HistoryProp(nameof(Name))]
        public IEnumerable<VersionedProp> Name_history { get; set; }
        [BindedProp]
        public double SellPrice { get; set; }
        [HistoryProp(nameof(SellPrice))]
        public IEnumerable<VersionedProp> SellPrice_history { get; set; }
        public IEnumerable<Provision> Provisions { get; set; } = new List<Provision>();
        [BindedProp]
        public string PartnerId { get; set; }
        [HistoryProp(nameof(PartnerId))]
        public IEnumerable<VersionedProp> PartnerId_history { get; set; }
    }
}
