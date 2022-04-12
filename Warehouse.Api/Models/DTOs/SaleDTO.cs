using System;
using System.Collections.Generic;
using System.Linq;

namespace Warehouse.Api.Models.DTOs
{
    public class SaleDTO : BaseDTO
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public IEnumerable<SaleItemDTO> SaleItems { get; set; } = Enumerable.Empty<SaleItemDTO>();
        public string PartnerId { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
