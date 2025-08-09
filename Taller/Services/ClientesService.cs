using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taller.Models;
using Microsoft.Extensions.Configuration;

namespace Taller.Services
{
    public class ClientesService
    {
        private readonly IMongoCollection<Cliente> _clientesCollection;

        public ClientesService(IConfiguration config)
        {
            var mongoSettings = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var client = new MongoClient(mongoSettings.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _clientesCollection = database.GetCollection<Cliente>("clientes");
        }

        public async Task<List<Cliente>> GetAsync() =>
            await _clientesCollection.Find(_ => true).ToListAsync();

        public async Task<Cliente> GetAsync(string id) =>
            await _clientesCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Cliente cliente) =>
            await _clientesCollection.InsertOneAsync(cliente);


        public async Task UpdateAsync(string id, Cliente cliente) =>
            await _clientesCollection.ReplaceOneAsync(c => c.Id == id, cliente);

        public async Task DeleteAsync(string id) =>
            await _clientesCollection.DeleteOneAsync(c => c.Id == id);
    }
}
