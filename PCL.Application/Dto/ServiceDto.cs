using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using PCL.Domain.Enums;
using PCL.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PCL.Application.Dto
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [JsonProperty("durationString")]
        public string DurationString { get; set; }
    }
}
