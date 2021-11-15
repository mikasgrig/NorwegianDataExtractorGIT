using System;
using System.Text.Json.Serialization;

namespace NorwegianDataExtractor.Models
{
    public class Day
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}