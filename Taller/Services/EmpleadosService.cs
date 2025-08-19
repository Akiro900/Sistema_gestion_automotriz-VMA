using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taller.Models;
using Microsoft.Extensions.Configuration;

namespace Taller.Services
{
    public class EmpleadosService
    {
        private readonly IMongoCollection<Empleado> _empleadosCollection;

        public EmpleadosService(IConfiguration config)
        {
            var mongoSettings = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var client = new MongoClient(mongoSettings.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _empleadosCollection = database.GetCollection<Empleado>("empleados");
        }

        public async Task<List<Empleado>> ObtenerTodosAsync() =>
            await _empleadosCollection.Find(_ => true).ToListAsync();

        public async Task<Empleado> ObtenerPorIdAsync(string id) =>
            await _empleadosCollection.Find(e => e.Id == id).FirstOrDefaultAsync();

        public async Task CrearAsync(Empleado empleado) =>
            await _empleadosCollection.InsertOneAsync(empleado);

        public async Task ActualizarAsync(string id, Empleado empleado) =>
            await _empleadosCollection.ReplaceOneAsync(e => e.Id == id, empleado);

        public async Task EliminarAsync(string id) =>
            await _empleadosCollection.DeleteOneAsync(e => e.Id == id);
    }
}
