using System.Collections.Generic;
using Warehouse.Database.Helpers;
using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Models
{
    public class Partner : BaseEntity
    {
        [BindedProp]
        public string Name { get; set; }
        [HistoryProp(nameof(Name))]
        public IEnumerable<VersionedProp> Name_history { get; set; }
        [BindedProp]
        public string VatNumber { get; set; }
        [HistoryProp(nameof(VatNumber))]
        public IEnumerable<VersionedProp> VatNumber_history { get; set; }
        [BindedProp]
        public string Address { get; set; }
        [HistoryProp(nameof(Address))]
        public IEnumerable<VersionedProp> Address_history { get; set; }
    }
}
