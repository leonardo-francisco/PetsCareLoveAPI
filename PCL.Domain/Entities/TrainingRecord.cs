﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Entities
{
    public class TrainingRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid TrainingId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid PetId { get; set; }
        public DateTime Date { get; set; }
        public string Evolution { get; set; }
    }
}
