using System;
using System.Threading.Tasks;
using Postcod.Models;

namespace Postcod.ExampleConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Location location = await new PostcodeLookupClient().Search("SN15 1HH");

            Console.WriteLine($"Latitude: {location.Latitude}; Longitude: {location.Longitude}");
            Console.ReadLine();
        }
    }
}