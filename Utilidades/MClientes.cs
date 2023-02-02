using Modelo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utilidades
{
    public class MClientes
    {
        public IEnumerable<Modelo.Cliente> GetAll()
        {
            Logica.MongoHelper.ConnectToMongoService();
            var filter = Builders<Modelo.Cliente>.Filter.Ne("_id", "");
            IMongoCollection<Modelo.Cliente> list = Logica.MongoHelper.database.GetCollection<Modelo.Cliente>("Clientes");
            var result = list.Find(filter).ToList();
            return result;
        }

        public void Insert(Modelo.Cliente Cliente)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Cliente> list = Logica.MongoHelper.database.GetCollection<Modelo.Cliente>("Clientes");
            Cliente.id = GenerateRandomId(24);
            list.InsertOneAsync(Cliente);
        }

        private static Random random = new Random();
        private Object GenerateRandomId(int v)
        {
            string starray = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(starray, v).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Modelo.Cliente GetOneById(string id)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Cliente> list = Logica.MongoHelper.database.GetCollection<Modelo.Cliente>("Clientes");
            var filter = Builders<Modelo.Cliente>.Filter.Eq("_id", id);
            var result = list.Find(filter).FirstOrDefault();
            return result;
        }

        public void Edit(string id, string nombre, string PApellido, string SApellido, string telefono, string correo)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Cliente> list = Logica.MongoHelper.database.GetCollection<Modelo.Cliente>("Clientes");
            var filter = Builders<Modelo.Cliente>.Filter.Eq("_id", id);
            var update = Builders<Modelo.Cliente>.Update
                .Set("nombre", nombre)
                .Set("PApellido", PApellido)
                .Set("SApellido", SApellido)
                .Set("telefono", telefono)
                .Set("correo", correo);
            var result = list.UpdateOneAsync(filter, update);
        }

        public void Delete(string id)
        {
            Logica.MongoHelper.ConnectToMongoService();
            IMongoCollection<Modelo.Cliente> list = Logica.MongoHelper.database.GetCollection<Modelo.Cliente>("Clientes");
            var filter = Builders<Modelo.Cliente>.Filter.Eq("_id", id);
            list.DeleteOneAsync(filter);
        }

        public async Task AgregarVehiculo(string id, Vehiculo vehiculo) {
            try {
                Logica.MongoHelper.ConnectToMongoService();
                IMongoCollection<Modelo.Cliente> list = Logica.MongoHelper.database.GetCollection<Modelo.Cliente>("Clientes");
                var filter = Builders<Modelo.Cliente>.Filter.Eq("_id", id);
                var update = Builders<Modelo.Cliente>.Update.Push("Reparaciones", vehiculo);
                var result = await list.FindOneAndUpdateAsync(filter, update);
            } catch (Exception ex) {
            
            } 
        }

        public List<Modelo.Vehiculo> GetAllVehiculos(string id)
        {
            List<Modelo.Vehiculo> lst = new List<Modelo.Vehiculo>();
            if (id != "0")
            {
                Logica.MongoHelper.ConnectToMongoService();
                IMongoCollection<Modelo.Cliente> list = Logica.MongoHelper.database.GetCollection<Modelo.Cliente>("Clientes");
                var filter = Builders<Modelo.Cliente>.Filter.Eq("_id", id);
                var result = list.Find(filter).FirstOrDefault();
                lst = result.Reparaciones;
                return lst;
            }
            else {
                return lst;
            }

        }
        public string AgregarReparacion(string cliente, string vehiculo, string tipo, string articulo, int cantidad)
        {
            Logica.MongoHelper.ConnectToMongoService();
            if (tipo == "1")
            {
                MRepuestos App = new MRepuestos();
                Modelo.Repuesto rep = App.GetOneById(articulo);
                if (rep.stock > cantidad)
                {
                    Modelo.Cobros n = new Modelo.Cobros();
                    n.descripcion = rep.descripcion;
                    n.cantidad = cantidad;
                    n.total = rep.precio_venta * cantidad;
                    IMongoCollection<Modelo.Cliente> list = Logica.MongoHelper.database.GetCollection<Modelo.Cliente>("Clientes");
                    var filter = Builders<Modelo.Cliente>.Filter.Where(x => x.id == cliente && x.Reparaciones.Any(i => i.placa == vehiculo));
                    var update = Builders<Modelo.Cliente>.Update.Push(x => x.Reparaciones[-1].Cobros, n);
                    var result = list.UpdateOneAsync(filter, update).Result;
                    rep.stock = rep.stock - cantidad;
                    string id = rep.id.ToString();
                    App.Edit(id,rep.descripcion,rep.stock,rep.precio_venta);
                    return "1";
                }
                else { return "2"; }
            }
            else if (tipo == "2")
            {
                MServicios App = new MServicios();
                Modelo.Servicio serv = App.GetOneById(articulo);
                Modelo.Cobros n = new Modelo.Cobros();
                n.descripcion = serv.descripcion;
                n.cantidad = cantidad;
                n.total = serv.precio_venta * cantidad;
                IMongoCollection<Modelo.Cliente> list = Logica.MongoHelper.database.GetCollection<Modelo.Cliente>("Clientes");
                var filter = Builders<Modelo.Cliente>.Filter.Where(x => x.id == cliente && x.Reparaciones.Any(i => i.placa == vehiculo));
                var update = Builders<Modelo.Cliente>.Update.Push(x => x.Reparaciones[-1].Cobros, n);
                var result = list.UpdateOneAsync(filter, update).Result;
                return "1";
            }
            else { return "3"; }
        }

        public IEnumerable<Modelo.Cobros> GetCobros(string cliente, string vehiculo)
        {
            IEnumerable<Modelo.Cobros> lista;
            try {
                Logica.MongoHelper.ConnectToMongoService();
                IMongoCollection<Modelo.Cliente> list = Logica.MongoHelper.database.GetCollection<Modelo.Cliente>("Clientes");
                var filter = Builders<Modelo.Cliente>.Filter.Where(x => x.id == cliente);
                Modelo.Vehiculo n = list.Find(filter).FirstOrDefault().Reparaciones.Find(x => x.placa == vehiculo);
                lista = n.Cobros.ToList();
                return lista;
            } catch(Exception ex) {
                lista = null;
                return lista;
            }
        }
    }
}
