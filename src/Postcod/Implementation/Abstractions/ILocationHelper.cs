using Postcod.Models;

namespace Postcod.Implementation.Abstractions
{
    internal interface ILocationHelper
    {
        bool IsValidUkPostcode(string postcode);
        double GetDistanceBetween(Location pointOne, Location pointTwo, DistanceUnit measure);
    }
}
