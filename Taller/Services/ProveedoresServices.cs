using MongoDB.Driver;
using Taller.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Taller.Services
{
    public class ProveedoresService
    {
        private readonly IMongoCollection<Proveedores> _proveedoresCollection;

        public ProveedoresService(IConfiguration config)
        {
            var mongoSettings = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var client = new MongoClient(mongoSettings.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _proveedoresCollection = database.GetCollection<Proveedores>("proveedores");
        }

        public async Task<List<Proveedores>> ObtenerTodosAsync() =>
            await _proveedoresCollection.Find(_ => true).ToListAsync();

        public async Task<Proveedores> ObtenerPorIdAsync(string id) =>
            await _proveedoresCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CrearAsync(Proveedores proveedor) =>
            await _proveedoresCollection.InsertOneAsync(proveedor);

        public async Task ActualizarAsync(string id, Proveedores proveedor) =>
            await _proveedoresCollection.ReplaceOneAsync(p => p.Id == id, proveedor);

        public async Task EliminarAsync(string id) =>
            await _proveedoresCollection.DeleteOneAsync(p => p.Id == id);
    }
}
