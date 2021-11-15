using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NorwegianDataExtractor.Models;

namespace NorwegianDataExtractor.Clients
{
    public class NorwegianClient : INorwegianClient
    {
        private const string CalendarAPIUrl = "https://www.norwegian.com/api/fare-calendar/calendar?adultCount=1&destinationAirportCode=FCO&originAirportCode=OSL&outboundDate=2021-12-01&tripType=1&currencyCode=USD&languageCode=en-US&pageId=258774&eventType=init";
        private readonly HttpClient _httpClient;
       
        public NorwegianClient()
        {
            _httpClient = new HttpClient();
        }
        public async Task<CalendarInfo> GetCalendarInfo()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
            _httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            
            var calendarInfo = await _httpClient.GetFromJsonAsync<CalendarInfo>(CalendarAPIUrl);
            var days = calendarInfo.Outbound.Days;
           
            return calendarInfo;
        }

        public async Task<HtmlDocument> GetFlightInfo(int day)
        {
            string FlithInfoUrl = $"https://www.norwegian.com/en/ipc/availability/avaday?AdultCount=1&A_City=FCO&D_City=OSL&D_Month=202112&D_Day={day}&IncludeTransit=false&TripType=1&CurrencyCode=USD&dFare=66";

            var response = await _httpClient.GetStringAsync(FlithInfoUrl);
            var norwegianHtmlDocument = new HtmlDocument();
            norwegianHtmlDocument.LoadHtml(response);
            return norwegianHtmlDocument;
        }
    }
}