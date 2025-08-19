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

        public async Task<List<Inventario>> ObtenerTodosAsync() =>
            await _inventarioCollection.Find(_ => true).ToListAsync();

        public async Task<Inventario> ObtenerPorIdAsync(string id) =>
            await _inventarioCollection.Find(i => i.Id == id).FirstOrDefaultAsync();

        public async Task CrearAsync(Inventario inventario) =>
            await _inventarioCollection.InsertOneAsync(inventario);

        public async Task ActualizarAsync(string id, Inventario inventario) =>
            await _inventarioCollection.ReplaceOneAsync(i => i.Id == id, inventario);

        public async Task EliminarAsync(string id) =>
            await _inventarioCollection.DeleteOneAsync(i => i.Id == id);
    }
}
