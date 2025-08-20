using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taller.Models;
using Microsoft.Extensions.Configuration;

namespace Taller.Services
{
    public class FacturasService
    {
        private readonly IMongoCollection<Factura> _facturasCollection;

        public FacturasService(IConfiguration config)
        {
            var mongoSettings = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var client = new MongoClient(mongoSettings.ConnectionURI);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _facturasCollection = database.GetCollection<Factura>("facturas");
        }

        public async Task<List<Factura>> GetAsync() =>
            await _facturasCollection.Find(_ => true).ToListAsync();

        

        public async Task<Factura> GetByIdAsync(string id) =>
            await _facturasCollection.Find(f => f.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Factura factura) =>
            await _facturasCollection.InsertOneAsync(factura);

        public async Task UpdateAsync(string id, Factura factura) =>
            await _facturasCollection.ReplaceOneAsync(f => f.Id == id, factura);

        public async Task DeleteAsync(string id) =>
            await _facturasCollection.DeleteOneAsync(f => f.Id == id);
    }
}
