using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Dto
{
    public class ExaminationDto
    {
        public Guid Id { get; set; }      
        public Guid PetId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
