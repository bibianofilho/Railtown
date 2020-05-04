using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Railtown.Interview.Api.Models;
using Railtown.Interview.Api.Validators;
using Xunit;

namespace Railtown.Interview.Tests
{
    public class GeoLocationValidatorTests
    {
        private readonly GeoLocationValidator _sut;

        public GeoLocationValidatorTests() => _sut = new GeoLocationValidator(new Mock<ILogger<GeoLocation>>().Object);


        [Fact]
        public void GeoLocationValidator_Fails_Latitude_OutOfRange()
        {
            //Arrange 
            var geoLocation = new GeoLocation()
            {
                Lat = -337.3159,
                Lng = 81.1496
            };

            //Act
            var actual = _sut.Validate(geoLocation);

            //Assert
            actual.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GeoLocationValidator_Fails_Longitude_OutOfRange()
        {
            //Arrange 
            var geoLocation = new GeoLocation()
            {
                Lat = -37.3159,
                Lng = 281.1496
            };

            //Act
            var actual = _sut.Validate(geoLocation);

            //Assert
            actual.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GeoLocationValidator_Succeeds_GeoLocation()
        {
            //Arrange 
            var geoLocation = new GeoLocation()
            {
                Lat = -37.3159,
                Lng = 81.1496
            };

            //Act
            var actual = _sut.Validate(geoLocation);

            //Assert
            actual.IsValid.Should().BeTrue();
        }
    }
}
