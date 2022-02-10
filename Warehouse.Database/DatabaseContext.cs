using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Warehouse.Database
{
    public class DatabaseContext : DbContext
    {
        protected readonly MongoClient _client;
        protected readonly IMongoDatabase _db;

        public DatabaseContext()
        {
            //change {database name} with database's name (curly brackets should not be included)
            _client = new MongoClient("mongodb+srv://FidoDidoo100:A123123123a@fbcluster.rdkdn.mongodb.net/warehouse?retryWrites=true&w=majority");
            //Change database name which you want to use
            _db = _client.GetDatabase("warehouse");
        }
    }
}
