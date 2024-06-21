using Mongo.Migration.Migrations.Document;
using MongoDB.Bson;
using MongoDB.Driver;
using TestandoSaporra.Context;
using TestandoSaporra.Models;

namespace TestandoSaporra.MongoMigration
{
    public static class MongoMigration
    {
        public static void ConfigureMigration(IConfiguration configuration)
        {
            var settings = new MongoDbSettings
            {
                ConnectionString = configuration.GetSection("MongoConnectionStrings:ConnectionString").Value,
                DatabaseName = configuration.GetSection("MongoConnectionStrings:DatabaseName").Value
            };

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            var migrator = new PetMigration();

            // Obtém todos os documentos da coleção "pets"
            var petsCollection = database.GetCollection<BsonDocument>("pets");
            var pets = petsCollection.Find(new BsonDocument()).ToList();

            // Executa a lógica de migração para cada documento
            foreach (var pet in pets)
            {
                migrator.Up(pet);
                // Ou para desfazer a migração:
                // migrator.Down(pet);
            }
        }
    }
}
