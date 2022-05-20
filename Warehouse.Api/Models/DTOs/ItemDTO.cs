﻿namespace Warehouse.Api.Models.DTOs
{
    public class ItemDTO : BaseDTO
    {
        public string Name { get; set; }
        public decimal Qtty { get; set; }
        public decimal BasePrice { get; set; }
        public decimal SellPrice { get; set; }
        public string PartnerId { get; set; }
    }
}
