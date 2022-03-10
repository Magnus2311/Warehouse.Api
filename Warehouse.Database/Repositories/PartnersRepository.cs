using Microsoft.Extensions.Configuration;
using Warehouse.Database.Models;

namespace Warehouse.Database.Repositories
{
    public class PartnersRepository : BaseRepository<Partner>
    {
        public PartnersRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
