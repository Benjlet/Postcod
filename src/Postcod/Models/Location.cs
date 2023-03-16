namespace Postcod.Models
{
    /// <summary>
    /// Location details.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// UK postcode.
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// Latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude.
        /// </summary>
        public double Longitude { get; set; }
    }
}