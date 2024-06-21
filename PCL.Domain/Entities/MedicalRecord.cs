using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Entities
{
    public class MedicalRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid AppointmentId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid VeterinarianId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid PetId { get; set; }
        public string MedicalHistory { get; set; }
        public string Medications { get; set; }
        public string Allergies { get; set; }
        public string Notes { get; set; }
    }
}
