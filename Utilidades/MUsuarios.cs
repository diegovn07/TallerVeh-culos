using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class MUsuarios
    {
        public IEnumerable<Modelo.Usuario> GetAll() {
            Logica.MongoHelper.ConnectToMongoService();
            var filter = Builders<Modelo.Usuario>.Filter.Ne("_id","");
            IMongoCollection<Modelo.Usuario> list = Logica.MongoHelper.database.GetCollection<Modelo.Usuario>("Usuarios");
            var result = list.Find(filter).ToList();
            return result;
        }

        public static object Loggin(string usuario, string contrasena)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Usuario> list = Logica.MongoHelper.database.GetCollection<Modelo.Usuario>("Usuarios");
            var filter = Builders<Modelo.Usuario>.Filter.Where(x => x.correo == usuario && x.contrasena == contrasena);
            var result = list.Find(filter).FirstOrDefault();
            return result;
        }

        public void Insert(Modelo.Usuario usuario)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Usuario> list = Logica.MongoHelper.database.GetCollection<Modelo.Usuario>("Usuarios");
            usuario.id = GenerateRandomId(24);
            list.InsertOneAsync(usuario);
        }

        private static Random random = new Random();
        private Object GenerateRandomId(int v)
        {
            string starray = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(starray, v).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Modelo.Usuario GetOneById(string id)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Usuario> list = Logica.MongoHelper.database.GetCollection<Modelo.Usuario>("Usuarios");
            var filter = Builders<Modelo.Usuario>.Filter.Eq("_id", id);
            var result = list.Find(filter).FirstOrDefault();
            return result;
        }

        public void Edit(string id, string nombre, string PApellido, string SApellido, string correo, string contrasena)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Usuario> list = Logica.MongoHelper.database.GetCollection<Modelo.Usuario>("Usuarios");
            var filter = Builders<Modelo.Usuario>.Filter.Eq("_id", id);
            var update = Builders<Modelo.Usuario>.Update
                .Set("nombre", nombre)
                .Set("PApellido", PApellido)
                .Set("SApellido", SApellido)
                .Set("correo", correo)
                .Set("contrasena", contrasena);
            var result = list.UpdateOneAsync(filter,update);
        }

        public void Delete(string id)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Usuario> list = Logica.MongoHelper.database.GetCollection<Modelo.Usuario>("Usuarios");
            var filter = Builders<Modelo.Usuario>.Filter.Eq("_id", id);
            list.DeleteOneAsync(filter);
        }

    }
}
