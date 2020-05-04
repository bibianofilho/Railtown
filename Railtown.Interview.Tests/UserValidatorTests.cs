using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Railtown.Interview.Api.Models;
using Railtown.Interview.Api.Validators;
using Xunit;

namespace Railtown.Interview.Tests
{
    public class UserValidatorTests
    {
        private readonly UserValidator _sut;

        public UserValidatorTests() => _sut = new UserValidator(new Mock<ILogger<User>>().Object);    

        [Fact]
        public void UserValidator_Fails_Name_Is_Empty()
        {
            //Arrange 
            var user = new User();

            //Act
            var actual = _sut.Validate(user);

            //Assert
            actual.IsValid.Should().BeFalse();
            actual.Errors.Should().Contain(e => e.ErrorMessage == $"'{nameof(User.Name)}' must not be empty.");
        }

        [Fact]
        public void UserValidator_Fails_Address_Is_Empty()
        {
            //Arrange 
            var user = new User();

            //Act
            var actual = _sut.Validate(user);

            //Assert
            actual.IsValid.Should().BeFalse();
            actual.Errors.Should().Contain(e => e.ErrorMessage == $"'{nameof(User.Address)}' must not be empty.");
        }

        [Fact]
        public void UserValidator_Succeeds_User()
        {
            //Arrange 
            var userLeanne = new User()
            {
                Id = 1,
                Name = "Leanne Graham",
                Email = "Sincere@april.biz",
                Address = new UserAdress()
                {
                    Street = "Kulas Light",
                    Geo = new GeoLocation()
                    {
                        Lat = -37.3159,
                        Lng = 81.1496
                    }
                }
            };

            //Act
            var actual = _sut.Validate(userLeanne);

            //Assert
            actual.IsValid.Should().BeTrue();
        }
    }
}
