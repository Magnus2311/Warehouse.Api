using MongoDB.Bson;

namespace Warehouse.Database.Interfaces
{
    public interface IEntity
    {
        public ObjectId Id { get; set; }
    }
}
