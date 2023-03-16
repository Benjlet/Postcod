using Postcod.Abstractions;
using Postcod.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Postcod.Extensions;

namespace Postcod.ExampleConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddPostcodesIOLookup(1000)
                .BuildServiceProvider();

            var client = serviceProvider.GetService<IPostcodeLookupClient>();

            var postcodeFrom = "SN15 1HH";
            var postcodeTo = "BA12 7PU";

            var fromResult = await client.Search(postcodeFrom);
            var toResult = await client.Search(postcodeTo);

            Console.WriteLine($"{postcodeFrom}: LATITUDE={fromResult.Latitude}; LONGITUDE={fromResult.Longitude}");
            Console.WriteLine($"{postcodeTo}: LATITUDE={toResult.Latitude}; LONGITUDE={toResult.Longitude}");

            var distance = client.GetDistanceBetween(fromResult, toResult, DistanceUnit.Kilometers);

            Console.WriteLine($"Distance between {postcodeFrom} and {postcodeTo}) is {distance}km");
            Console.ReadLine();
        }
    }
}