namespace Warehouse.Api.Models.DTOs
{
    public class SaleItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Qtty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string ItemId { get; set; }
    }
}
