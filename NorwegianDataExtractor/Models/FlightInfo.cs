using System;

namespace NorwegianDataExtractor.Models
{
    public class FlightInfo
    {
        public string DepartureAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime ArrivalTime { get; set; }
        
        public decimal Price { get; set; }
        public override string ToString()
        {
            return @$"Departure Airport: {DepartureAirport}
Departure Time: {DepartureTime}
Arrival Airport: {ArrivalAirport}
Arrival Time: {ArrivalTime}
Price: {Price}";
        }
    }
}