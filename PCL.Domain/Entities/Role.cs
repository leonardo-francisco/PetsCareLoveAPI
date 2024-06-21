using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Entities
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [BsonRepresentation(BsonType.String)]
        public List<Guid>? PermissionsIds { get; set; } = new List<Guid>();
    }
}
