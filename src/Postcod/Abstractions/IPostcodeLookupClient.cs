using Postcod.Models;
using System.Threading.Tasks;

namespace Postcod
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
    }
}