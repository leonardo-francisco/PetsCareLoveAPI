using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PCL.Domain.Entities;
using PCL.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Dto
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }    
        public Guid OwnerId { get; set; }     
        public Guid PetId { get; set; }
        
        public Guid ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; }
        public string Notes { get; set; }

       
        public Guid? VeterinarianId { get; set; }       
        public Guid? TrainerId { get; set; }       
        public Guid? EmployeeId { get; set; }

        //public OwnerDto Owner { get; set; }
        //public PetDto Pet { get; set; }
        //public ServiceDto Service { get; set; }
        //public VeterinarianDto Veterinarian { get; set; }
        //public TrainerDto Trainer { get; set; }
        //public EmployeeDto Employee { get; set; }
    }
}
