using Postcod.Models;
using System.Threading.Tasks;

namespace Postcod.Implementation.Abstractions
{
    internal interface IPostcodeLookupService
    {
        Task<Location> Search(string postcode);
    }
}