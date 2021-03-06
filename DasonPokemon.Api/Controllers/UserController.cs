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

        [HttpPost("linkAccount")]
        public async Task<ActionResult> LinkAccount(LinkAccountServiceModel link)
        {
            var result = await _userService.LinkAccount(link);
            return result.WasSuccessful ? Ok() : BadRequest(result.FailureReason);
        }
    }
}
