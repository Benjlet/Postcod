using Postcod.Abstractions;
using Postcod.Exceptions;
using Postcod.Implementation;
using Postcod.Implementation.Abstractions;
using Postcod.Models;
using Moq;
using NUnit.Framework;

namespace Postcod.Tests
{
    internal class PostcodeLookupClientTests
    {
        private PostcodeLookupClient _sut;
        private Mock<IPostcodeLookupService> _postcodeLookupService;
        private Mock<ILocationHelper> _locationHelper;

        [SetUp]
        public void Setup()
        {
            _postcodeLookupService = new Mock<IPostcodeLookupService>();
            _locationHelper = new Mock<ILocationHelper>();

            _sut = new PostcodeLookupClient(_postcodeLookupService.Object, _locationHelper.Object);
        }

        [Test]
        public void GetDistanceBetween_CallsDependencies()
        {
            _locationHelper.Setup(x => x.GetDistanceBetween(It.IsAny<Location>(), It.IsAny<Location>(), DistanceUnit.Kilometers)).Returns(10000);

            var res = _sut.GetDistanceBetween(new Location()
            {
                Latitude = 51.462139,
                Longitude = -2.118380
            },
            new Location()
            {
                Latitude = 51.187592,
                Longitude = -2.232880
            }, DistanceUnit.Kilometers);

            _locationHelper.Verify(x => x.GetDistanceBetween(It.IsAny<Location>(), It.IsAny<Location>(), DistanceUnit.Kilometers), Times.Once);
        }

        [Test]
        public void Search_InvalidPostcode_ThrowsException()
        {
            var postcode = "SN15 1HJ";

            _locationHelper.Setup(x => x.IsValidUkPostcode(It.IsAny<string>())).Returns(false);

            var res = _sut.Search(postcode);

            Assert.ThrowsAsync<PostcodeLookupValidationException>(() => _sut.Search(postcode));
        }

        [Test]
        public void Search_ValidPostcode_CallDependencies()
        {
            var postcode = "SN15 1HJ";

            _locationHelper.Setup(x => x.IsValidUkPostcode(It.IsAny<string>())).Returns(true);
            _postcodeLookupService.Setup(x => x.Search(It.IsAny<string>())).ReturnsAsync(new Location()
            {
                Latitude = 51.187592,
                Longitude = -2.232880
            });

            var res = _sut.Search(postcode);

            _locationHelper.Verify(x => x.IsValidUkPostcode(It.IsAny<string>()), Times.Once);
            _postcodeLookupService.Verify(x => x.Search(It.IsAny<string>()), Times.Once);
        }
    }
}
