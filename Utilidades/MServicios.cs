using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Utilidades
{
    public class MServicios
    {
        public IEnumerable<Modelo.Servicio> GetAll()
        {
            Logica.MongoHelper.ConnectToMongoService();
            var filter = Builders<Modelo.Servicio>.Filter.Ne("_id", "");
            IMongoCollection<Modelo.Servicio> list = Logica.MongoHelper.database.GetCollection<Modelo.Servicio>("Servicios");
            var result = list.Find(filter).ToList();
            return result;
        }

        public void Insert(Modelo.Servicio Servicio)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Servicio> list = Logica.MongoHelper.database.GetCollection<Modelo.Servicio>("Servicios");
            Servicio.id = GenerateRandomId(24);
            list.InsertOneAsync(Servicio);
        }

        private static Random random = new Random();
        private Object GenerateRandomId(int v)
        {
            string starray = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(starray, v).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Modelo.Servicio GetOneById(string id)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Servicio> list = Logica.MongoHelper.database.GetCollection<Modelo.Servicio>("Servicios");
            var filter = Builders<Modelo.Servicio>.Filter.Eq("_id", id);
            var result = list.Find(filter).FirstOrDefault();
            return result;
        }

        public void Edit(string id, string descripcion, double precio_venta)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Servicio> list = Logica.MongoHelper.database.GetCollection<Modelo.Servicio>("Servicios");
            var filter = Builders<Modelo.Servicio>.Filter.Eq("_id", id);
            var update = Builders<Modelo.Servicio>.Update
                .Set("descripcion", descripcion)
                .Set("precio_venta", precio_venta);
            var result = list.UpdateOneAsync(filter, update);
        }

        public void Delete(string id)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Servicio> list = Logica.MongoHelper.database.GetCollection<Modelo.Servicio>("Servicios");
            var filter = Builders<Modelo.Servicio>.Filter.Eq("_id", id);
            list.DeleteOneAsync(filter);
        }

    }
}
