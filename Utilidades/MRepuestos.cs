using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Utilidades
{
    public class MRepuestos
    {
        public IEnumerable<Modelo.Repuesto> GetAll()
        {
            Logica.MongoHelper.ConnectToMongoService();
            var filter = Builders<Modelo.Repuesto>.Filter.Ne("_id", "");
            IMongoCollection<Modelo.Repuesto> list = Logica.MongoHelper.database.GetCollection<Modelo.Repuesto>("Repuestos");
            var result = list.Find(filter).ToList();
            return result;
        }

        public void Insert(Modelo.Repuesto repuesto)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Repuesto> list = Logica.MongoHelper.database.GetCollection<Modelo.Repuesto>("Repuestos");
            repuesto.id = GenerateRandomId(24);
            list.InsertOneAsync(repuesto);
        }

        private static Random random = new Random();
        private Object GenerateRandomId(int v)
        {
            string starray = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(starray, v).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Modelo.Repuesto GetOneById(string id)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Repuesto> list = Logica.MongoHelper.database.GetCollection<Modelo.Repuesto>("Repuestos");
            var filter = Builders<Modelo.Repuesto>.Filter.Eq("_id", id);
            var result = list.Find(filter).FirstOrDefault();
            return result;
        }

        public void Edit(string id, string descripcion, int stock, double precio_venta)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Repuesto> list = Logica.MongoHelper.database.GetCollection<Modelo.Repuesto>("Repuestos");
            var filter = Builders<Modelo.Repuesto>.Filter.Eq("_id", id);
            var update = Builders<Modelo.Repuesto>.Update
                .Set("descripcion", descripcion)
                .Set("stock", stock)
                .Set("precio_venta", precio_venta);
            var result = list.UpdateOneAsync(filter, update);
        }

        public void Delete(string id)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Repuesto> list = Logica.MongoHelper.database.GetCollection<Modelo.Repuesto>("Repuestos");
            var filter = Builders<Modelo.Repuesto>.Filter.Eq("_id", id);
            list.DeleteOneAsync(filter);
        }

    }
}
