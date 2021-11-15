using System.Collections.Generic;
using System.Threading.Tasks;
using NorwegianDataExtractor.Models;

namespace NorwegianDataExtractor.Service
{
    public interface IServiceNorwegian
    {
        List<int> GetFlightDays(CalendarInfo model);
        string GetFlightDate(CalendarInfo model, int day);
        Task<List<FlightInfo>> GetFlithInfo();
    }
}