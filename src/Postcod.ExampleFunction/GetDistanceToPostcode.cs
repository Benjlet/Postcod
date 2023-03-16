using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Postcod.Abstractions;
using Postcod.Models;

namespace Postcod.ExampleFunction
{
    public class GetDistanceToPostcode
    {
        private readonly IPostcodeLookupClient _postcodeLookupClient;

        public GetDistanceToPostcode(IPostcodeLookupClient postcodeLookupClient)
        {
            _postcodeLookupClient = postcodeLookupClient;
        }

        [FunctionName("GetDistanceToPostcode")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "distance/{postcodeFrom}/{postcodeTo}}")] HttpRequest req, string postcodeFrom, string postcodeTo)
        {
            var fromPostcod = await _postcodeLookupClient.Search(postcodeFrom);
            var toPostcod = await _postcodeLookupClient.Search(postcodeTo);

            var distanceKilometers = _postcodeLookupClient.GetDistanceBetween(fromPostcod, toPostcod, DistanceUnit.Kilometers);

            return new OkObjectResult(distanceKilometers);
        }
    }
}
