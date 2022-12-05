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
            var Database = client.GetDatabase(configuration.GetValue<string>("DataBaseSetting:Databse"));
            Products = Database.GetCollection<Product>(configuration.GetValue<string>("DataBaseSetting:Collection"));
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
