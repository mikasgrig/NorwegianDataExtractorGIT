using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NorwegianDataExtractor.Models
{
    public class Outbound
    {
        [JsonPropertyName("days")]
        public List<Day> Days { get; set; }
    }
}