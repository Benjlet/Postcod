using Postcod.Exceptions;
using Postcod.Implementation.Abstractions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System;
using Postcod.Implementation.LookupServices.PostcodesIO;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Net;

namespace Postcod.Tests
{
    public class PostcodesIOServiceTests
    {
        private PostcodesIOService _sut;
        private Mock<IHttpClientWrapper> _httpClientWrapper;

        [SetUp]
        public void Setup()
        {
            _httpClientWrapper = new Mock<IHttpClientWrapper>();
            _sut = new PostcodesIOService(_httpClientWrapper.Object);
        }

        [Test]
        public async Task PostcodesIO_ValidResponse_ShouldParse()
        {
            var postcode = "SN15 1HH";
            var latitude = "51.461018";
            var longitude = "-2.118283";
            var country = "England";
            var ward = "Chippenham Hardens & Central";
            var region = "South West";
            var northings = 173567;
            var eastings = 391879;
            var district = "Wiltshire";
            var parish = "Chippenham";

            _httpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(
                $"{{ \"status\":200,\"result\":{{ \"postcode\":\"{postcode}\",\"quality\":1,\"eastings\":{eastings},\"northings\":{northings},\"country\":\"{country}\",\"nhs_ha\":\"South West\",\"longitude\":{longitude},\"latitude\":{latitude},\"european_electoral_region\":\"South West\",\"primary_care_trust\":\"Wiltshire\",\"region\":\"{region}\",\"lsoa\":\"Wiltshire 011B\",\"msoa\":\"Wiltshire 011\",\"incode\":\"1HH\",\"outcode\":\"SN15\",\"parliamentary_constituency\":\"Chippenham\",\"admin_district\":\"{district}\",\"parish\":\"{parish}\",\"admin_county\":null,\"date_of_introduction\":\"198001\",\"admin_ward\":\"{ward}\",\"ced\":null,\"ccg\":\"NHS Bath and North East Somerset, Swindon and Wiltshire\",\"nuts\":\"Wiltshire\",\"pfa\":\"Wiltshire\",\"codes\":{{ \"admin_district\":\"E06000054\",\"admin_county\":\"E99999999\",\"admin_ward\":\"E05013419\",\"parish\":\"E04013038\",\"parliamentary_constituency\":\"E14000635\",\"ccg\":\"E38000231\",\"ccg_id\":\"92G\",\"ced\":\"E99999999\",\"nuts\":\"TLK15\",\"lsoa\":\"E01031912\",\"msoa\":\"E02006654\",\"lau2\":\"E06000054\",\"pfa\":\"E23000038\"}} }} }}");

            var searchResult = await _sut.Search(postcode);

            Assert.Multiple(() =>
            {
                Assert.That(searchResult.Postcode, Is.EqualTo(postcode));
                Assert.That(searchResult.Latitude.ToString(), Is.EqualTo(latitude));
                Assert.That(searchResult.Longitude.ToString(), Is.EqualTo(longitude));
                Assert.That(searchResult.Northings, Is.EqualTo(northings));
                Assert.That(searchResult.Eastings, Is.EqualTo(eastings));
                Assert.That(searchResult.Ward, Is.EqualTo(ward));
                Assert.That(searchResult.Region, Is.EqualTo(region));
                Assert.That(searchResult.District, Is.EqualTo(district));
                Assert.That(searchResult.Parish, Is.EqualTo(parish));
                Assert.That(searchResult.Country, Is.EqualTo(country));
            });
        }

        [Test]
        public void PostcodesIO_ApiError_ShouldParseErrorJson()
        {
            var postcode = "SN15 1HH";
            var errorMessage = "This is an error.";
            var errorJson = $"{{ \"status\":400,\"error\":\"{errorMessage}\"}}";

            _httpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(errorJson);

            var exception = Assert.ThrowsAsync<PostcodeLookupResponseException>(() => _sut.Search(postcode));

            Assert.That(exception.Message, Is.EqualTo(errorMessage));
        }

        [Test]
        public void PostcodesIO_InvalidResponse_ShouldThrowException()
        {
            var postcode = "SN15 1HH";
            var invalidResponse = "Not an expected response";

            _httpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(invalidResponse);

            Assert.ThrowsAsync<PostcodeLookupResponseException>(() => _sut.Search(postcode));
        }

        [Test]
        public void PostcodesIO_FailedHttpCall_ShouldThrowResponseException()
        {
            var postcode = "SN15 1HH";
            var innerExceptionText = "Inner exception text";

            _httpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new TimeoutException(innerExceptionText));

            Assert.ThrowsAsync<PostcodeLookupSearchException>(() => _sut.Search(postcode));
        }
    }
}