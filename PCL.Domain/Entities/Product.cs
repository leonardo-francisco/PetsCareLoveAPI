using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Photo {  get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid CategoryId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid PetId { get; set; }
    }
}
