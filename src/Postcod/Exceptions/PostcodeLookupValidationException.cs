using System;

namespace Postcod.Exceptions
{
    /// <summary>
    /// Represents a postcode validation error before the postcode lookup service is called.
    /// </summary>
    public class PostcodeLookupValidationException : Exception
    {
        /// <summary>
        /// Initialises a new PostcodeLookupValidationException with the supplied inner exception.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public PostcodeLookupValidationException(Exception innerException) :
            base("Error parsing postcode. Please check inner exception for details.", innerException)
        {

        }
    }
}