using Railtown.Interview.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Railtown.Interview.Api.Clients
{
    public interface IUsersApiClient
    {
        Task<List<User>> GetUsers();
    }
}
