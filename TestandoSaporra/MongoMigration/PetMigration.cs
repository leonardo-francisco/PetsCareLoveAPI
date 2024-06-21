using Mongo.Migration.Migrations.Document;
using MongoDB.Bson;
using MongoDB.Driver;
using TestandoSaporra.Models;

namespace TestandoSaporra.MongoMigration
{
    public class PetMigration : DocumentMigration<Pet>
    {
        public PetMigration() : base("0.1.0")
        {
        }

        public override void Up(BsonDocument document)
        {
            // Lógica de migração para o Up
            if (!document.Contains("DateOfBirth"))
            {
                document.Add("DateOfBirth", BsonNull.Value);
            }
        }

        public override void Down(BsonDocument document)
        {
            // Lógica de migração para o Down
            document.Remove("DateOfBirth");
        }
    }
}
