using Postcod.Models;
using System.Threading.Tasks;

namespace Postcod.Abstractions
{
    /// <summary>
    /// The main client that validates and calls into the underlying postcode lookup service.
    /// </summary>
    public interface IPostcodeLookupClient
    {
        /// <summary>
        /// Searches the supplied postcode and returns Location details.
        /// </summary>
        /// <param name="postcode">The postcode to search.</param>
        /// <returns>Location details.</returns>
        public Task<Location> Search(string postcode);

        /// <summary>
        /// Gets the distance, in the requested unit, between two locations with Longitude and Latitude data.
        /// </summary>
        /// <param name="pointOne">The first location.</param>
        /// <param name="pointTwo">The second location.</param>
        /// <param name="measure">The unit of the returned distance value.</param>
        /// <returns>The distance between the two points, in the request unit.</returns>
        public double GetDistanceBetween(Location pointOne, Location pointTwo, DistanceUnit measure = DistanceUnit.Kilometers);
    }
}