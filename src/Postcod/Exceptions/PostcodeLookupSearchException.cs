using System;

namespace Postcod.Exceptions
{
    /// <summary>
    /// Represents an error calling the postcode service.
    /// </summary>
    public class PostcodeLookupSearchException : Exception
    {
        /// <summary>
        /// Initialises a new PostcodeLookupSearchException with the supplied inner exception.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public PostcodeLookupSearchException(Exception innerException) :
            base("Error calling postcode service. Please check inner exception for details.", innerException)
        {

        }
    }
}