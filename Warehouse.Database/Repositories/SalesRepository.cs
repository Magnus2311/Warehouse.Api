using Microsoft.Extensions.Configuration;
using Warehouse.Database.Models;

namespace Warehouse.Database.Repositories
{
    public class SalesRepository : BaseRepository<Sale>
    {
        public SalesRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
