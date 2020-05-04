using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Railtown.Interview.Api.Services;

namespace Railtown.Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("FarthestUsers")]
        public async Task<IActionResult> GetFarthestUsers()
        {
            var users = await _userService.GetUsers().ConfigureAwait(false);
            var farthestUsers = _userService.GetFarthestUsers(users);
            return Ok(farthestUsers);
        }
    }
}
