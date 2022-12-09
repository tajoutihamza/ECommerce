using Catalog.API.Data.Interface;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DataBaseSetting:CnxString"));
            var Database = client.GetDatabase(configuration.GetValue<string>("DataBaseSetting:DataBase"));
            Products = Database.GetCollection<Product>(configuration.GetValue<string>("DataBaseSetting:Collection"));
        }
        public IMongoCollection<Product> Products { get; }
    }
}
