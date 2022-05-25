using Microsoft.Extensions.Configuration;
using Warehouse.Database.Helpers;
using Warehouse.Database.Models;

namespace Warehouse.Database.Repositories
{
    public class SalesRepository : BaseRepository<Sale>
    {
        public SalesRepository(IConfiguration configuration, AppSettings appSettings) : base(configuration, appSettings)
        {

        }
    }
}
