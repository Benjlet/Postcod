using Postcod.Implementation;
using Postcod.Models;
using Moq;
using NUnit.Framework;
using Postcod.Implementation.Abstractions;

namespace Postcod.Tests
{
    internal class PostcodeLookupClientTests
    {
        private PostcodeLookupClient _sut;
        private Mock<IPostcodeLookupService> _postcodeLookupService;

        [SetUp]
        public void Setup()
        {
            _postcodeLookupService = new Mock<IPostcodeLookupService>();

            _sut = new PostcodeLookupClient(_postcodeLookupService.Object);
        }

        [Test]
        public void Search_ValidPostcode_CallDependencies()
        {
            var postcode = "SN15 1HJ";

            _postcodeLookupService.Setup(x => x.Search(It.IsAny<string>())).ReturnsAsync(new Location()
            {
                Latitude = 51.187592,
                Longitude = -2.232880
            });

            var res = _sut.Search(postcode);

            _postcodeLookupService.Verify(x => x.Search(It.IsAny<string>()), Times.Once);
        }
    }
}
