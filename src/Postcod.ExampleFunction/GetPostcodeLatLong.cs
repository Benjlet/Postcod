using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Postcod.Abstractions;

namespace Postcod.ExampleFunction
{
    public class GetPostcodeLatLong
    {
        private readonly IPostcodeLookupClient _postcodeLookupClient;

        public GetPostcodeLatLong(IPostcodeLookupClient postcodeLookupClient)
        {
            _postcodeLookupClient = postcodeLookupClient;
        }

        [FunctionName("GetPostcodeLatLong")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "postcode/{postcode}")] HttpRequest req, string postcode)
        {
            var result = await _postcodeLookupClient.Search(postcode);

            return new OkObjectResult($"{result.Latitude},{result.Longitude}");
        }
    }
}
