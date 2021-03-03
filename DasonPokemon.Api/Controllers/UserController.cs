using System.Threading.Tasks;
using DasonPokemon.Core.Models;
using DasonPokemon.Core.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace DasonPokemon.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateUser(UserServiceModel user)
        {
            var result = await _userService.CreateUser(user);
            return result.WasSuccessful ? Ok() : BadRequest(result.FailureReason);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserServiceModel user)
        {
            var result = await _userService.Authenticate(user.Email, user.Password);
            return result.WasSuccessful ? Ok() : BadRequest(result.FailureReason);
        }
    }
}
