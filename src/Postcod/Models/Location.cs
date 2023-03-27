using System.Dynamic;

namespace Postcod.Models
{
    /// <summary>
    /// Location details.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// The searched UK postcode.
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// The WGS84 latitude of the searched postcode.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// The WGS84 longitude of the searched postcode.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// UK parish of the searched postcode.
        /// </summary>
        public string Parish { get; set; }

        /// <summary>
        /// UK region code for the searched postcode.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Northing OS grid reference for the searched postcode.
        /// </summary>
        public int? Northings { get; set; }

        /// <summary>
        /// Easting OS grid reference for the searched postcode.
        /// </summary>
        public int? Eastings { get; set; }

        /// <summary>
        /// Assigned district of the searched postcode.
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Constituent country of the searched postcode.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Assigned ward of the searched postcode.
        /// </summary>
        public string Ward { get; set; }
    }
}