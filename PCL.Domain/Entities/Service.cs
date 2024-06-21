using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PCL.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCL.Domain.Utils;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PCL.Domain.Entities
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public ServiceType Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [JsonProperty("durationString")]
        public string DurationString { get; set; }
    }
}
