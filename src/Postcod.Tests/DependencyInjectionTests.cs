using Postcod.Abstractions;
using Postcod.Extensions;
using Postcod.Implementation.Abstractions;
using Postcod.Implementation.LookupServices.PostcodesIO;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Postcod.Tests
{
    public class DependencyInjectionTests
    {
        [Test]
        public void Dependencies_PostcodesIO_ShouldBeCreated()
        {
            var serviceProvider = new ServiceCollection()
                .AddPostcodesIOLookup()
                .BuildServiceProvider();

            var client = serviceProvider.GetService<IPostcodeLookupClient>();
            var service = serviceProvider.GetService<IPostcodeLookupService>();
            var httpClientWrapper = serviceProvider.GetService<IHttpClientWrapper>();
            var helper = serviceProvider.GetService<ILocationHelper>();

            Assert.Multiple(() =>
            {
                Assert.That(client, Is.Not.Null);
                Assert.That(service, Is.Not.Null);
                Assert.That(httpClientWrapper, Is.Not.Null);
                Assert.That(helper, Is.Not.Null);
            });

            Assert.That(service.GetType(), Is.EqualTo(typeof(PostcodesIOService)));
        }
    }
}