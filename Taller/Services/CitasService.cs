using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taller.Models;
using Microsoft.Extensions.Configuration;

namespace Taller.Services
{
    public class CitasService
    {
        private readonly IMongoCollection<Citas> _citasCollection;

        public CitasService(IConfiguration config)
        {
            var mongoSettings = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var client = new MongoClient(mongoSettings.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _citasCollection = database.GetCollection<Citas>("citas");
        }

        public async Task<List<Citas>> GetAsync() =>
            await _citasCollection.Find(_ => true).ToListAsync();

        public async Task<Citas> GetByIdAsync(string id) =>
            await _citasCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Citas cita) =>
            await _citasCollection.InsertOneAsync(cita);

        public async Task UpdateAsync(string id, Citas cita) =>
            await _citasCollection.ReplaceOneAsync(c => c.Id == id, cita);

        public async Task DeleteAsync(string id) =>
            await _citasCollection.DeleteOneAsync(c => c.Id == id);
    }
}
