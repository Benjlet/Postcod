using Postcod.Exceptions;
using Postcod.Implementation.Abstractions;
using Postcod.Models;
using System;
using System.Text.RegularExpressions;

namespace Postcod.Implementation
{
    internal class LocationHelper : ILocationHelper
    {
        private static readonly string _ukPostcodeRegexPattern = "^(GIR 0AA)|[a-z-[qvx]](?:\\d|\\d{2}|[a-z-[qvx]]\\d|[a-z-[qvx]]\\d[a-z-[qvx]]|[a-z-[qvx]]\\d{2})(?:\\s?\\d[a-z-[qvx]]{2})?$";
        private static readonly Regex _ukPostcodeRegex = new(_ukPostcodeRegexPattern, RegexOptions.Compiled, TimeSpan.FromSeconds(3));

        public bool IsValidUkPostcode(string postcode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(postcode) || postcode.Length > 10)
                {
                    return false;
                }

                postcode = postcode.Trim().ToLower();

                return _ukPostcodeRegex.Match(postcode).Success;
            }
            catch (Exception ex)
            {
                throw new PostcodeLookupValidationException(ex);
            }
        }

        public double GetDistanceBetween(Location pointOne, Location pointTwo, DistanceUnit measure)
        {
            if (pointOne?.Latitude == null || pointOne?.Longitude == null || pointTwo?.Latitude == null || pointTwo?.Longitude == null
                || pointOne.Latitude.Equals(pointTwo.Latitude) && pointOne.Longitude.Equals(pointTwo.Longitude))
            {
                return 0;
            }

            var theta = pointOne.Longitude.Value - pointTwo.Longitude.Value;

            var distance =
                Math.Sin(DegreesToRadians(pointOne.Latitude.Value)) *
                Math.Sin(DegreesToRadians(pointTwo.Latitude.Value)) +
                Math.Cos(DegreesToRadians(pointOne.Latitude.Value)) *
                Math.Cos(DegreesToRadians(pointTwo.Latitude.Value)) *
                Math.Cos(DegreesToRadians(theta));

            distance = Math.Acos(distance);
            distance = distance / Math.PI * 180.0;
            distance = distance * 60 * 1.1515;

            if (measure == DistanceUnit.Kilometers)
            {
                distance *= 1.609344;
            }

            return Math.Round(distance, 1);
        }

        private double DegreesToRadians(double deg) =>
            deg * Math.PI / 180.0;
    }
}
