using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taller.Models;
using Microsoft.Extensions.Configuration;

namespace Taller.Services
{
    public class ServiciosService
    {
        private readonly IMongoCollection<Servicio> _serviciosCollection;

        public ServiciosService(IConfiguration config)
        {
            var mongoSettings = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var client = new MongoClient(mongoSettings.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _serviciosCollection = database.GetCollection<Servicio>("servicios");
        }

        public async Task<List<Servicio>> ObtenerTodosAsync() =>
            await _serviciosCollection.Find(_ => true).ToListAsync();

        public async Task<Servicio> ObtenerPorIdAsync(string id) =>
            await _serviciosCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async Task CrearAsync(Servicio servicio) =>
            await _serviciosCollection.InsertOneAsync(servicio);

        public async Task ActualizarAsync(string id, Servicio servicio) =>
            await _serviciosCollection.ReplaceOneAsync(s => s.Id == id, servicio);

        public async Task EliminarAsync(string id) =>
            await _serviciosCollection.DeleteOneAsync(s => s.Id == id);
    }
}
