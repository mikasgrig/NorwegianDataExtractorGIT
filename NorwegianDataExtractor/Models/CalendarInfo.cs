using System;
using System.Text.Json.Serialization;

namespace NorwegianDataExtractor.Models
{
    public class CalendarInfo
    {
        [JsonPropertyName("outbound")]
        public Outbound Outbound { get; set; }

    }
}