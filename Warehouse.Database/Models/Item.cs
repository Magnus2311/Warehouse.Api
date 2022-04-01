using System.Collections.Generic;
using Warehouse.Database.Helpers;
using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Models
{
    public class Item : BaseEntity
    {
        [BindedProp]
        public string Name { get; set; }
        public IEnumerable<VersionedProp<dynamic>> Name_history { get; set; }
        [BindedProp]
        public double BasePrice { get; set; }
        public IEnumerable<VersionedProp<dynamic>> BasePrice_history { get; set; }
        [BindedProp]
        public double SellPrice { get; set; }
        public IEnumerable<VersionedProp<dynamic>> SellPrice_history { get; set; }
    }
}
