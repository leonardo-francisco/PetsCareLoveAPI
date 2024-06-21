using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PCL.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid Role { get; set; }
        
    }
}
