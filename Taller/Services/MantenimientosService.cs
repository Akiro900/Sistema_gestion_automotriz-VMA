using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taller.Models;
using Microsoft.Extensions.Configuration;

namespace Taller.Services
{
    public class MantenimientosService
    {
        private readonly IMongoCollection<Mantenimiento> _mantenimientosCollection;

        public MantenimientosService(IConfiguration config)
        {
            // Obtener configuración desde appsettings.json
            var mongoSettings = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var client = new MongoClient(mongoSettings.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _mantenimientosCollection = database.GetCollection<Mantenimiento>("mantenimientos");
        }

        // Obtener todos los mantenimientos
        public async Task<List<Mantenimiento>> GetAsync() =>
            await _mantenimientosCollection.Find(_ => true).ToListAsync();

        // Obtener mantenimiento por Id
        public async Task<Mantenimiento> GetByIdAsync(string id) =>
            await _mantenimientosCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

        // Crear nuevo mantenimiento
        public async Task CreateAsync(Mantenimiento mantenimiento) =>
            await _mantenimientosCollection.InsertOneAsync(mantenimiento);

        // Actualizar mantenimiento
        public async Task UpdateAsync(string id, Mantenimiento mantenimiento) =>
            await _mantenimientosCollection.ReplaceOneAsync(m => m.Id == id, mantenimiento);

        // Eliminar mantenimiento
        public async Task DeleteAsync(string id) =>
            await _mantenimientosCollection.DeleteOneAsync(m => m.Id == id);
    }
}
