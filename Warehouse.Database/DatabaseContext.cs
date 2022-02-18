using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Warehouse.Database
{
    public class DatabaseContext : DbContext
    {
        protected readonly MongoClient _client;
        protected readonly IMongoDatabase _db;
        private readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DatabaseContext()
        {
            //change {database name} with database's name (curly brackets should not be included)
            _client = new MongoClient(_configuration.GetConnectionString("DefaultConnection"));
            //Change database name which you want to use
            _db = _client.GetDatabase("warehouse");
        }
    }
}
