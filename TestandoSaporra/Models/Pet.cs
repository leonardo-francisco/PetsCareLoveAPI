using Mongo.Migration.Documents;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestandoSaporra.Models
{
    public class Pet : IDocument
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        [BsonIgnore]
        public DocumentVersion Version { get; set; }
    }
}
