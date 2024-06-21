using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PCL.Domain.Entities
{
    public class Breed
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid TypeAnimalId { get; set; }
        public TypeAnimal TypeAnimal { get; set; }
        
    }
}
