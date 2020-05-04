using FluentAssertions;
using Moq;
using Railtown.Interview.Api.Clients;
using Railtown.Interview.Api.Models;
using Railtown.Interview.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Railtown.Interview.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _sut;

        private readonly Mock<IUsersApiClient> _mockUsersApiClient;
        public UserServiceTests()
        {
            _mockUsersApiClient = new Mock<IUsersApiClient>();
            _sut = new UserService(_mockUsersApiClient.Object);
        }

        [Fact]
        public async Task UserService_Succeeds_GetUsers()
        {
            //Arrange
            var expectedResult = new List<User>();
            expectedResult.Add(new User()
            {
                Id = 1,
                Name = "Leanne Graham",
                Email = "Sincere@april.biz",
                Address = new UserAdress()
                {
                    Street = "Kulas Light",
                    Geo = new GeoLocation() {
                        Lat = -37.3159,
                        Lng = 81.1496
                    }
                }
            });
            _mockUsersApiClient.Setup(u => u.GetUsers())
                .ReturnsAsync(expectedResult);

            //Act
            var result = await _sut.GetUsers().ConfigureAwait(false);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);

        }
        
        [Fact]
        public void UserService_Fails_GetFarthestUsers_Users_Not_Provided()
        {
            //Act
            Action act = () => _sut.GetFarthestUsers(null);

            //Assert
            act.Should().Throw<ArgumentNullException>("Value cannot be null. (Parameter 'users')");
        }

        [Fact]
        public void UserService_Fails_GetFarthestUsers_Users_Less_than_Two()
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
            var users = new List<User>();
            users.Add(userLeanne);
            //Act
            Action act = () => _sut.GetFarthestUsers(users);

            //Assert
            act.Should().Throw<ArgumentException>("At least, two users are required");
        }

        [Fact]
        public void UserService_Succeeds_GetFarthestUsers()
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
            var userErvin = new User()
            {
                Id = 2,
                Name = "Ervin Howell",
                Email = "Shanna@melissa.tv",
                Address = new UserAdress()
                {
                    Street = "Kulas Light",
                    Geo = new GeoLocation()
                    {
                        Lat = -43.9509,
                        Lng = -34.4618
                    }
                }
            };
            var userClementine = new User()
            {
                Id = 3,
                Name = "Clementine Bauch",
                Email = "Nathan@yesenia.net",
                Address = new UserAdress()
                {
                    Street = "Kulas Light",
                    Geo = new GeoLocation()
                    {
                        Lat = -68.6102,
                        Lng = -47.0653
                    }
                }
            };
            var users = new List<User>();
            users.Add(userLeanne);
            users.Add(userErvin);
            users.Add(userClementine);
            var resultExpected = new UsersDistance()
            {
                UserA = userLeanne,
                UserB = userErvin,
                Distance = 8905960.4016543031
            };

            //Act
            var result = _sut.GetFarthestUsers(users);

            //Assert
            result.UserA.Id.Should().Be(resultExpected.UserA.Id);
            result.UserB.Id.Should().Be(resultExpected.UserB.Id);
            result.Distance.Should().Be(resultExpected.Distance);

        }
    }
}
