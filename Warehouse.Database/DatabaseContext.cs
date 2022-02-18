using Microsoft.Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Web;

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

            var settings = MongoClientSettings.FromConnectionString($"mongodb+srv://{_configuration["DB_Username"]}:{_configuration["DB_Password"]}@{_configuration["DB_Cluster"]}.rdkdn.mongodb.net/warehouse?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            _client = new MongoClient(settings);

            _db = _client.GetDatabase("warehouse");
        }
    }
}
