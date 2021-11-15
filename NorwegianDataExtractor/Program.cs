using System;
using System.Threading.Tasks;
using NorwegianDataExtractor.Clients;
using NorwegianDataExtractor.Service;


namespace NorwegianDataExtractor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var norwegianClient = new NorwegianClient();
            var _serviceNorwegian = new ServiceNorwegian(norwegianClient);
            
            var flightInfo = await _serviceNorwegian.GetFlithInfo();

            foreach (var item in flightInfo)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}