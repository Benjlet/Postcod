using Postcod.Models;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Postcod.Implementation.PostcodesIO;
using Postcod.Implementation.Abstractions;
using Postcod.Implementation;

namespace Postcod
{
    /// <summary>
    /// The main postcode lookup client that validates and calls into the underlying postcode lookup service.
    /// </summary>
    public class PostcodeLookupClient : IPostcodeLookupClient
    {
        private const string PostcodeServiceUrl = "https://postcodes.io/";
        private const long TimeoutMilliseconds = 10000;

        private readonly IPostcodeLookupService _postcodeLookupService;

        /// <summary>
        /// Initialises a new <see cref="PostcodeLookupClient"/> as an underlying HttpClient, with the default Postcodes IO address and timeout.
        /// </summary>
        public PostcodeLookupClient()
        {
            var httpClientWrapper = new HttpClientWrapper(new HttpClient()
            {
                BaseAddress = new Uri(PostcodeServiceUrl),
                Timeout = TimeSpan.FromMilliseconds(TimeoutMilliseconds)
            });

            _postcodeLookupService = new PostcodesIOService(httpClientWrapper);
        }

        /// <summary>
        /// Initialises a new <see cref="PostcodeLookupClient"/> as an underlying HttpClient, with the supplied base address and timeout.
        /// </summary>
        /// <param name="baseAddress">Base address of the PostcodesIO API</param>
        /// <param name="timeoutMilliseconds">Timeout (milliseconds).</param>
        public PostcodeLookupClient(string baseAddress = PostcodeServiceUrl, long timeoutMilliseconds = TimeoutMilliseconds)
        {
            var httpClientWrapper = new HttpClientWrapper(new HttpClient()
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds)
            });

            _postcodeLookupService = new PostcodesIOService(httpClientWrapper);
        }

        /// <summary>
        /// Initialises a new <see cref="PostcodeLookupClient"/> with the supplied HttpClient - this should have its address set to the PostcodesIO API.
        /// </summary>
        /// <param name="httpClient">The underlying HttpClient for calls to the PostcodesIO API.</param>
        public PostcodeLookupClient(HttpClient httpClient)
        {
            var httpClientWrapper = new HttpClientWrapper(httpClient);
            _postcodeLookupService = new PostcodesIOService(httpClientWrapper);
        }

        internal PostcodeLookupClient(
            IPostcodeLookupService postcodeLookupService)
        {
            _postcodeLookupService = postcodeLookupService;
        }

        /// <inheritdoc/>
        public async Task<Location> Search(string postcode)
        {
            return await _postcodeLookupService.Search(postcode).ConfigureAwait(false);
        }
    }
}