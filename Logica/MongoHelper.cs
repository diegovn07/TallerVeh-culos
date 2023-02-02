using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Logica
{
    public class MongoHelper
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoConnection = "mongodb+srv://diego:Diego.131090@cluster0-cj3fz.mongodb.net/test?retryWrites=true&w=majority";
        public static string MongoDataBase = "RepuestosMundiales";

        public static void ConnectToMongoService() {
            try {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDataBase);
            } catch (Exception ex) { }
        }
    }
}
