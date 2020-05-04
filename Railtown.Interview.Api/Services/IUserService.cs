using Railtown.Interview.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Railtown.Interview.Api.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        UsersDistance GetFarthestUsers(List<User> users);
    }
}