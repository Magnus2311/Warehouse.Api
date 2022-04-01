namespace Warehouse.Database.Models
{
    public class SaleItem : BaseEntity
    {
        public string Name { get; set; }
        public decimal Qtty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string ItemId { get; set; }
    }
}
