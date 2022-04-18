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

            _client = new MongoClient($"mongodb+srv://{_configuration["DB_Username"]}:{_configuration["DB_Password"]}{_configuration["DB_Cluster"]}.rdkdn.mongodb.net/warehouse?retryWrites=true&w=majority");

            bool.TryParse(_configuration["IsTesting"], out var isTesting);
            string additionalCollection = isTesting ? $"-{_configuration["TestingCollection"]}" : "";
            _db = _client.GetDatabase($"warehouse{additionalCollection}");
        }
    }
}
