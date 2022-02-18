using Microsoft.Extensions.Configuration;
using Warehouse.Database.Models;

namespace Warehouse.Database.Repositories
{
    public class ItemsRepository : BaseRepository<Item>
    {
        public ItemsRepository(IConfiguration configuration) : base(configuration)
        {
                
        }
    }
}
