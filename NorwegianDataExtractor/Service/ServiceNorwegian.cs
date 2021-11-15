using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using NorwegianDataExtractor.Clients;
using NorwegianDataExtractor.Models;

namespace NorwegianDataExtractor.Service
{
    public class ServiceNorwegian : IServiceNorwegian
    {
        private readonly INorwegianClient _norwegianClient;

        public ServiceNorwegian(INorwegianClient norwegianClient)
        {
            _norwegianClient = norwegianClient;
        }

        public List<int> GetFlightDays(CalendarInfo model)
        {
            var flightDays = new List<int>();
            int dayInt = 1;
            foreach ( Day day in model.Outbound.Days)
            {
                if (day.Price != 0)
                {
                    flightDays.Add(dayInt);
                }
                dayInt++;
            }

            return flightDays;
        }

        public string GetFlightDate(CalendarInfo model, int day)
        {
            var datenew = new DateTime(1999, 01, 01);
            
            foreach (var date in model.Outbound.Days)
            {
                if (day == date.Date.Day)
                {
                    datenew = date.Date;
                }
            }
            var dateParse = datenew.ToString("yyy-MM-dd");

            return dateParse;
        }

        public async Task<List<FlightInfo>> GetFlithInfo()
        {
            var calendarInfo = await _norwegianClient.GetCalendarInfo();

            var flightDays = GetFlightDays(calendarInfo);
            var flightInfo = new List<FlightInfo>();

            foreach (int day in flightDays)
            {

                var fligthDate = GetFlightDate(calendarInfo, day);
                var htmlDocument = await _norwegianClient.GetFlightInfo(day);
                var nodeDeparture = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"avaday-outbound-result\"]/div/div/div[2]/div/table/tbody/tr[1]/td[1]/div");
                var departureTime = nodeDeparture.Select(node => node.InnerText).FirstOrDefault();
                var nodeArrival = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"avaday-outbound-result\"]/div/div/div[2]/div/table/tbody/tr[1]/td[2]/div");
                var arrivalTime = nodeArrival.Select(node => node.InnerText).FirstOrDefault();
                var nodePrice = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"avaday-outbound-result\"]/div/div/div[2]/div/table/tbody/tr[1]/td[5]/div/label");
                var lowPrice = nodePrice.Select(node => node.InnerText).FirstOrDefault();
                var nodeDepartureAirport = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"avaday-outbound-result\"]/div/div/div[2]/div/table/tbody/tr[2]/td[1]/div");
                var departureAirport = nodeDepartureAirport.Select(node => node.InnerText).FirstOrDefault();
                var nodeArrivalAirport = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"avaday-outbound-result\"]/div/div/div[2]/div/table/tbody/tr[2]/td[2]/div");
                var arrivalAirport = nodeArrivalAirport.Select(node => node.InnerText).FirstOrDefault();
                
                
                var flight = new FlightInfo
                {
                    ArrivalAirport = arrivalAirport,
                    DepartureAirport = departureAirport,
                    ArrivalTime = DateTime.ParseExact($"{fligthDate} {arrivalTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                    DepartureTime = DateTime.ParseExact($"{fligthDate} {departureTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                    Price = Convert.ToDecimal(lowPrice)
                };
                flightInfo.Add(flight);
                }
            return flightInfo;
        }
    }
}