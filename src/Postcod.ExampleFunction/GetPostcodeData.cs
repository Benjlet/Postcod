using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Postcod.ExampleFunction
{
    public class GetPostcodeData
    {
        private readonly IPostcodeLookupClient _postcodeLookupClient;
        private readonly ILogger _logger;

        public GetPostcodeData(
            IPostcodeLookupClient postcodeLookupClient,
            ILogger<GetPostcodeData> logger)
        {
            _postcodeLookupClient = postcodeLookupClient;
            _logger = logger;
        }

        [Function(nameof(GetPostcodeData))]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "postcode/{postcode}")] HttpRequestData req, string postcode)
        {
            _logger.LogInformation($"Searching details for postcode '{postcode}'...");

            var result = await _postcodeLookupClient.Search(postcode);

            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(new
            {
                latitude = result.Latitude,
                longitude = result.Longitude
            });

            return response;
        }
    }
}
