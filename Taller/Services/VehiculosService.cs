using MongoDB.Driver;
using System.Numerics;
using Taller.Models;

namespace Taller.Services
{
    public class VehiculosService
    {
        private readonly IMongoCollection<Vehiculo> _vehiculosCollection;

        public VehiculosService(IConfiguration config)
        {
            var mongoSettings = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var client = new MongoClient(mongoSettings.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _vehiculosCollection = database.GetCollection<Vehiculo>("vehiculos");
        }

        public async Task<List<Vehiculo>> GetAsync() =>
            await _vehiculosCollection.Find(_ => true).ToListAsync();

        public async Task<Vehiculo> GetByIdAsync(string id) =>
            await _vehiculosCollection.Find(v => v.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Vehiculo vehiculo) =>
            await _vehiculosCollection.InsertOneAsync(vehiculo);

        public async Task UpdateAsync(string id, Vehiculo vehiculo) =>
            await _vehiculosCollection.ReplaceOneAsync(v => v.Id == id, vehiculo);

        public async Task DeleteAsync(string id) =>
            await _vehiculosCollection.DeleteOneAsync(v => v.Id == id);

        // Devuelve un vehículo por su placa
        public async Task<Vehiculo> GetByPlacaAsync(string placa) =>
            await _vehiculosCollection.Find(v => v.Placa == placa).FirstOrDefaultAsync();

        public async Task<List<Vehiculo>> GetAllAsync() =>
    await _vehiculosCollection.Find(_ => true).ToListAsync();

        // Últimos N vehículos registrados
        public async Task<List<Vehiculo>> GetUltimosAsync(int cantidad) =>
            await _vehiculosCollection.Find(_ => true)
                                      .Limit(cantidad)
                                      .ToListAsync();

    }
}
