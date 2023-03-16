using System;

namespace Postcod.Exceptions
{
    /// <summary>
    /// Represents an error translating the postcode service response.
    /// </summary>
    public class PostcodeLookupResponseException : Exception
    {
        /// <summary>
        /// Initialises a new PostcodeLookupResponseException with the supplied inner exception.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public PostcodeLookupResponseException(Exception innerException) :
            base("Error translating postcode service response. Please check inner exception for details.", innerException)
        {

        }
    }
}