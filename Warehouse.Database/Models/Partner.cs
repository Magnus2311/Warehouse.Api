using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Models
{
    public class Partner : BaseEntity
    {
        [BindedProp]
        public string Name { get; set; }
        [BindedProp]
        public string VatNumber { get; set; }
        [BindedProp]
        public string Address { get; set; }
    }
}
