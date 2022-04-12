namespace Warehouse.Api.Models.DTOs
{
    public abstract class BaseDTO
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}