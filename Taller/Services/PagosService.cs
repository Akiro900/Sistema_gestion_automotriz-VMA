using MongoDB.Driver;
using Taller.Models;
using Microsoft.Extensions.Configuration;

namespace Taller.Services
{
    public class PagosService
    {
        private readonly IMongoCollection<Pago> _pagosCollection;

        public PagosService(IConfiguration config)
        {
            // Obtener configuración desde appsettings.json
            var configuracionMongo = config.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            var cliente = new MongoClient(configuracionMongo.ConnectionURI);
            var baseDatos = cliente.GetDatabase(configuracionMongo.DatabaseName);
            _pagosCollection = baseDatos.GetCollection<Pago>("pagos");
        }

        // Obtener todos los pagos
        public List<Pago> ObtenerTodos() => _pagosCollection.Find(p => true).ToList();

        // Obtener un pago por ID
        public Pago ObtenerPorId(string id) => _pagosCollection.Find(p => p.Id == id).FirstOrDefault();

        // Crear un pago
        public void Crear(Pago pago) => _pagosCollection.InsertOne(pago);

        // Editar un pago
        public void Editar(string id, Pago pago) =>
            _pagosCollection.ReplaceOne(p => p.Id == id, pago);

        // Eliminar un pago
        public void Eliminar(string id) =>
            _pagosCollection.DeleteOne(p => p.Id == id);
    }
}
