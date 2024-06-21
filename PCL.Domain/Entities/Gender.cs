using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PCL.Domain.Entities
{
    public class Gender 
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        
    }
}
