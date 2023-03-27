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
            var responseJson = await GetLocationDetailsJson(postcode).ConfigureAwait(false);
            return HandleResponse(responseJson);
        }

        private async Task<string> GetLocationDetailsJson(string postcode)
        {
            try
            {
                return await _httpClientWrapper.GetAsync($"postcodes/{postcode}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new PostcodeLookupSearchException(ex);
            }
        }

        private Location HandleResponse(string json)
        {
            PostcodesIOResponse responseDetails;

            try
            {
                responseDetails = JsonConvert.DeserializeObject<PostcodesIOResponse>(json);
            }
            catch (Exception ex)
            {
                throw new PostcodeLookupResponseException(ex);
            }

            if (responseDetails?.status != 200)
            {
                var errorMessage = TryGetError(json);

                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    throw new PostcodeLookupResponseException(errorMessage);
                }
            }

            return new Location()
            {
                Postcode = responseDetails?.result?.postcode,
                Latitude = responseDetails?.result?.latitude,
                Longitude = responseDetails?.result?.longitude,
                Country = responseDetails?.result?.country,
                District = responseDetails?.result?.admin_district,
                Eastings = responseDetails?.result?.eastings,
                Northings = responseDetails?.result?.northings,
                Parish = responseDetails?.result?.parish,
                Region = responseDetails?.result?.region,
                Ward = responseDetails?.result?.admin_ward
            };
        }

        private string TryGetError(string json)
        {
            try
            {
                var errorDetails = JsonConvert.DeserializeObject<Error>(json);
                return errorDetails?.error ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}