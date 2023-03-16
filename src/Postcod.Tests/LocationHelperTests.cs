using Postcod.Implementation;
using Postcod.Models;
using NUnit.Framework;
using System;

namespace Postcod.Tests
{
    internal class LocationHelperTests
    {
        private LocationHelper _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new LocationHelper();
        }

        [Test]
        public void GetDistanceBetween_Miles_IsCorrect()
        {
            var distance = _sut.GetDistanceBetween(new Location()
            {
                Latitude = 51.462139,
                Longitude = -2.118380
            },
            new Location()
            {
                Latitude = 51.187592,
                Longitude = -2.232880
            }, DistanceUnit.Miles);

            Assert.That(Math.Round(distance, 1), Is.EqualTo(19.6));
        }

        [Test]
        public void GetDistanceBetween_Kilometers_IsCorrect()
        {
            var distance = _sut.GetDistanceBetween(new Location()
            {
                Latitude = 51.462139,
                Longitude = -2.118380
            },
            new Location()
            {
                Latitude = 51.187592,
                Longitude = -2.232880
            }, DistanceUnit.Kilometers);

            Assert.That(Math.Round(distance, 1), Is.EqualTo(31.5));
        }

        [Test]
        [TestCase("SN15")]
        [TestCase("SN151HJ")]
        [TestCase("EC4R 1BR")]
        [TestCase("SN15 1HJ")]
        public void IsValidUkPostcode_GivenValid_ReturnsTrue(string postcode)
        {
            var isValid = _sut.IsValidUkPostcode(postcode);
            Assert.That(isValid, Is.True);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("SEnOneFive")]
        [TestCase("S N 1 5 1 H J")]
        [TestCase("EC 4R 1 BR")]
        [TestCase("  SN15 1 HJ")]
        public void IsValidUkPostcode_GivenInvalid_ReturnsFalse(string postcode)
        {
            var isValid = _sut.IsValidUkPostcode(postcode);
            Assert.That(isValid, Is.False);
        }
    }
}
