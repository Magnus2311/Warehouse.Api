namespace Warehouse.Api.Models.DTOs
{
    public class BuyItemDTO
    {
        public string ItemId { get; set; }
        public decimal Qtty { get; set; }
        public decimal BasePrice { get; set; }
    }
}