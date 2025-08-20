using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taller.Models;
using Microsoft.Extensions.Configuration;

namespace Taller.Services
{
    public class InventarioService
    {
        private readonly IMongoCollection<Inventario> _inventarioCollection;

        public InventarioService(IConfiguration config)
        {
            var mongoSettings = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var client = new MongoClient(mongoSettings.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _inventarioCollection = database.GetCollection<Inventario>("inventario");
        }

        public async Task<List<Inventario>> GetAsync() =>
            await _inventarioCollection.Find(_ => true).ToListAsync();

        public async Task<Inventario> GetAsync(string id) =>
            await _inventarioCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Inventario inventario) =>
            await _inventarioCollection.InsertOneAsync(inventario);


        public async Task UpdateAsync(string id, Inventario inventario) =>
            await _inventarioCollection.ReplaceOneAsync(c => c.Id == id, inventario);

        public async Task DeleteAsync(string id) =>
            await _inventarioCollection.DeleteOneAsync(c => c.Id == id);
    }
}
