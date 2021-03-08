using System;
using System.Threading.Tasks;
using DasonPokemon.Core.Entities;
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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(Guid id)
        {
            var result = await _userService.GetUser(id);
            return result == null ? NotFound() : Ok();
        }

        [HttpGet("byEmail/{email}")]
        public async Task<ActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUser(email);
            return result == null ? NotFound() : Ok();
        }

        [HttpPost("create")]
        public async Task<ActionResult<User>> CreateUser(UserServiceModel user)
        {
            var result = await _userService.Create(user);
            return Ok(result);
        }

        [HttpPost("linkAccount")]
        public async Task<ActionResult> LinkAccount(LinkAccountServiceModel link)
        {
            var result = await _userService.LinkAccount(link);
            return result.WasSuccessful ? Ok() : BadRequest(result.FailureReason);
        }

        [HttpPost("updateCollection")]
        public async Task<ActionResult> UpdateCollectionList(CollectionServiceModel collection)
        {
            await _userService.UpdateCollection(collection);
            return Ok();
        }
    }
}
