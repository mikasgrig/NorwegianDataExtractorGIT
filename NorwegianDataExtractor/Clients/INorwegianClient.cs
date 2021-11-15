using System.Threading.Tasks;
using HtmlAgilityPack;
using NorwegianDataExtractor.Models;

namespace NorwegianDataExtractor.Clients
{
    public interface INorwegianClient
    {
        Task<CalendarInfo> GetCalendarInfo();
        Task<HtmlDocument> GetFlightInfo(int day);
    }
}