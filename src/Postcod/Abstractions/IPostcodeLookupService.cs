using Postcod.Models;
using System.Threading.Tasks;

namespace Postcod.Abstractions
{
    /// <summary>
    /// Handles the interaction between the postcode lookup service - calls, validates, and returns the search response.
    /// </summary>
    public interface IPostcodeLookupService
    {
        /// <summary>
        /// Searches the supplied postcode and returns Location details.
        /// </summary>
        /// <param name="postcode">The postcode to search.</param>
        /// <returns>Location details.</returns>
        public Task<Location> Search(string postcode);
    }
}