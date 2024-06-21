using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PCL.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Entities
{
    public class Appointment
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid OwnerId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid PetId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public string Notes { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid? VeterinarianId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid? TrainerId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid? EmployeeId { get; set; }

        public Owner Owner { get; set; }
        public Pet Pet { get; set; }
        public Service Service { get; set; }
        public Veterinarian Veterinarian { get; set; }
        public Trainer Trainer { get; set; }
        public Employee Employee { get; set; }
    }
}
