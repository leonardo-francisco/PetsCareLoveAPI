using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestandoSaporra.Models;

namespace TestandoSaporra.Context
{
    public class PetCareContext
    {
        private readonly IMongoDatabase _database;

        public PetCareContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Pet> Pets => _database.GetCollection<Pet>("pets");
       
    }
}
