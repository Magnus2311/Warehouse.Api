using System.Collections.Generic;
using Warehouse.Database.Helpers;
using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Models
{
    public class Partner : BaseEntity
    {
        [BindedProp]
        public string Name { get; set; }
        public IEnumerable<VersionedProp> Name_history { get; set; }
        [BindedProp]
        public string VatNumber { get; set; }
        public IEnumerable<VersionedProp> VatNumber_history { get; set; }
        [BindedProp]
        public string Address { get; set; }
        public IEnumerable<VersionedProp> Address_history { get; set; }
    }
}
