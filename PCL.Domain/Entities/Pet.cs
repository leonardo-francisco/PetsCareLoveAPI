using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace PCL.Domain.Entities
{
    public class Pet
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Photo { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid BreedId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid GenderId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid OwnerId { get; set; }

        [JsonIgnore]
        [BsonIgnore]
        public Breed Breed { get; set; }

        [JsonIgnore]
        [BsonIgnore]
        public Gender Gender { get; set; }

        [JsonIgnore]
        [BsonIgnore]
        public Owner Owner { get; set; }


    }
}
