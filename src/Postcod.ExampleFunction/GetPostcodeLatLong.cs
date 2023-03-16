using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Postcod.Abstractions;

namespace Postcod.ExampleFunction
{
    public class GetPostcodePostcod
    {
        private readonly IPostcodeLookupClient _postcodeLookupClient;

        public GetPostcodePostcod(IPostcodeLookupClient postcodeLookupClient)
        {
            _postcodeLookupClient = postcodeLookupClient;
        }

        [FunctionName("GetPostcodePostcod")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "postcode/{postcode}")] HttpRequest req, string postcode)
        {
            var result = await _postcodeLookupClient.Search(postcode);

            return new OkObjectResult($"{result.Latitude},{result.Longitude}");
        }
    }
}
