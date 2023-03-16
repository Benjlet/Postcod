using Postcod.Abstractions;
using Postcod.Models;
using System;
using System.Threading.Tasks;
using Postcod.Exceptions;
using Postcod.Implementation.Abstractions;

namespace Postcod.Implementation
{
    internal class PostcodeLookupClient : IPostcodeLookupClient
    {
        private readonly IPostcodeLookupService _postcodeLookupService;
        private readonly ILocationHelper _locationHelper;

        public PostcodeLookupClient(
            IPostcodeLookupService postcodeLookupService,
            ILocationHelper locationHelper)
        {
            _postcodeLookupService = postcodeLookupService;
            _locationHelper = locationHelper;
        }

        public async Task<Location> Search(string postcode)
        {
            if (!_locationHelper.IsValidUkPostcode(postcode))
            {
                throw new PostcodeLookupValidationException(
                    new ArgumentException("Postcode is not in a valid UK format.", nameof(postcode)));
            }

            return await _postcodeLookupService.Search(postcode);
        }

        public double GetDistanceBetween(Location pointOne, Location pointTwo, DistanceUnit measure = DistanceUnit.Miles)
        {
            return _locationHelper.GetDistanceBetween(pointOne, pointTwo, measure);
        }
    }
}