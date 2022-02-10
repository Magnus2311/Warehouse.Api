namespace Warehouse.Api.Models.DTOs
{
    public class ItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double BasePrice { get; set; }
        public double SellPrice { get; set; }
    }
}
