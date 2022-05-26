using Microsoft.Extensions.Configuration;
using Warehouse.Database.Helpers;
using Warehouse.Database.Models;

namespace Warehouse.Database.Repositories
{
    public class PartnersRepository : BaseRepository<Partner>
    {
        public PartnersRepository(IConfiguration configuration, AppSettings appSettings) : base(configuration, appSettings)
        {

        }
    }
}
