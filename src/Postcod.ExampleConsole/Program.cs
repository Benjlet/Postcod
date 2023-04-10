using System;
using System.Threading.Tasks;
using Postcod.Implementation;

namespace Postcod.ExampleConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = new PostcodeLookupClient(timeoutMilliseconds: 100000);

            var postcode = "SN15 1HH";
            var location = await client.Search(postcode);

            Console.WriteLine($"{postcode}: LATITUDE={location.Latitude}; LONGITUDE={location.Longitude}");
            Console.ReadLine();
        }
    }
}