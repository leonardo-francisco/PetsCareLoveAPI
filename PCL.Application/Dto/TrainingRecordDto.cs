using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Dto
{
    public class TrainingRecordDto
    {
        public Guid Id { get; set; }
        public Guid TrainingId { get; set; }
        public Guid PetId { get; set; }
        public DateTime Date { get; set; }
        public string Evolution { get; set; }
    }
}
