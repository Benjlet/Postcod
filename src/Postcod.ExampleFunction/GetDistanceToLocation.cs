using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Postcod.Abstractions;
using Postcod.Models;

namespace Postcod.ExampleFunction
{
    public class GetDistanceToLocation
    {
        private readonly IPostcodeLookupClient _postcodeLookupClient;

        public GetDistanceToLocation(IPostcodeLookupClient postcodeLookupClient)
        {
            _postcodeLookupClient = postcodeLookupClient;
        }

        [FunctionName("GetDistanceToLocation")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "distance/{postcode}/{latitude}/{longitude}}")] HttpRequest req, string postcode, string latitude, string longitude)
        {
            if (!double.TryParse(latitude, out var toLatitude))
            {
                return new BadRequestObjectResult("Latitude must be in decimal format: {latitude}/{longitude}");
            }

            if (!double.TryParse(longitude, out var toLongitude))
            {
                return new BadRequestObjectResult("Longitude must be in decimal format: {latitude}/{longitude}");
            }

            var toPostcod = new Location()
            {
                Latitude = toLatitude,
                Longitude = toLongitude
            };

            var fromPostcod = await _postcodeLookupClient.Search(postcode);
            var distanceKilometers = _postcodeLookupClient.GetDistanceBetween(fromPostcod, toPostcod, DistanceUnit.Kilometers);

            return new OkObjectResult(distanceKilometers);
        }
    }
}
