using Postcod.Abstractions;
using Postcod.Exceptions;
using Postcod.Implementation.Abstractions;
using Postcod.Implementation.LookupServices.PostcodesIO.Json;
using Postcod.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Postcod.Implementation.LookupServices.PostcodesIO
{
    internal class PostcodesIOService : IPostcodeLookupService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public PostcodesIOService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<Location> Search(string postcode)
        {
            var responseJson = await GetLocationDetailsJson(postcode);
            return HandleResponse(responseJson);
        }

        private async Task<string> GetLocationDetailsJson(string postcode)
        {
            try
            {
                return await _httpClientWrapper.GetAsync($"postcodes/{postcode}");
            }
            catch (Exception ex)
            {
                throw new PostcodeLookupSearchException(ex);
            }
        }

        private Location HandleResponse(string json)
        {
            try
            {
                var details = JsonConvert.DeserializeObject<Root>(json);

                return new Location()
                {
                    Postcode = details.result.postcode,
                    Latitude = details.result.latitude,
                    Longitude = details.result.longitude
                };
            }
            catch (Exception ex)
            {
                throw new PostcodeLookupResponseException(ex);
            }
        }
    }
}