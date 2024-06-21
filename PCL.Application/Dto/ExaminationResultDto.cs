using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Dto
{
    public class ExaminationResultDto
    {
        public Guid Id { get; set; }
        public Guid ExaminationId { get; set; }
        public string Result { get; set; }
        public string Notes { get; set; }
    }
}
