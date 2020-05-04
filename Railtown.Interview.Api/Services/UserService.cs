using GeoCoordinatePortable;
using Railtown.Interview.Api.Clients;
using Railtown.Interview.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Railtown.Interview.Api.Services
{
    public class UserService : IUserService
    {
        public readonly IUsersApiClient _usersApiClient;
        public UserService(IUsersApiClient usersApiClient)
        {
            _usersApiClient = usersApiClient ?? throw new ArgumentNullException(nameof(usersApiClient));
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _usersApiClient.GetUsers().ConfigureAwait(false);
            return users;
        }

        public UsersDistance GetFarthestUsers(List<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }
            else if (users.Count < 2)
            {
                throw new ArgumentException("At least, two users are required");
            }

            var userDistance = new List<UsersDistance>();
            var listCount = users.Count;
            for (int i = 0; i < listCount; i++)
            {
                for (int y = i + 1; y < listCount; y++)
                {
                    var locA = new GeoCoordinate(users[i].Address.Geo.Lat, users[i].Address.Geo.Lng);
                    var locB = new GeoCoordinate(users[y].Address.Geo.Lat, users[y].Address.Geo.Lng);
                    double distance = locA.GetDistanceTo(locB); // meters
                    System.Diagnostics.Debug.WriteLine($" User A {users[i].Name} and UserB {users[i].Name} distance {distance}");

                    userDistance.Add(new UsersDistance() { UserA = users[i], UserB = users[y], Distance = distance });
                }
            }
            var userDistanceInDescOrder = userDistance.OrderByDescending(d => d.Distance).ToList();//Are really rare two users with the same distance, otherwise group and get the list
            return userDistanceInDescOrder[0];
        }
    }
}
