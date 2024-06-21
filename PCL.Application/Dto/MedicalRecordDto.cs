using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Dto
{
    public class MedicalRecordDto
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid VeterinarianId { get; set; }
        public Guid PetId { get; set; }
        public string MedicalHistory { get; set; }
        public string Medications { get; set; }
        public string Allergies { get; set; }
        public string Notes { get; set; }
    }
}
