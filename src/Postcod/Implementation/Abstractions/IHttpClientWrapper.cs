using System.Threading.Tasks;

namespace Postcod.Implementation.Abstractions
{
    internal interface IHttpClientWrapper
    {
        Task<string> GetAsync(string uri);
    }
}