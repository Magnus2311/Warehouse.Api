namespace Warehouse.Database.Models
{
    public class Partner : BaseEntity
    {
        public string Name { get; set; }
        public string VatNumber { get; set; }
        public string Address { get; set; }
    }
}
