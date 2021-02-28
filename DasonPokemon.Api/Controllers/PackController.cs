using DasonPokemon.Core;
using DasonPokemon.Core.Entities;
using DasonPokemon.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DasonPokemon.Api.Controllers
{
    [ApiController]
    [Route("api/packs")]
    public class PackController : ControllerBase
    {
        private readonly IPackService _packService;

        public PackController(IPackService packService)
        {
            _packService = packService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pack>>> GetPacks()
        {
            return Ok(await _packService.GetAllPacks());
        }

        [HttpGet("generate/{id}")]
        public async Task<ActionResult<IEnumerable<Card>>> GeneratePack(Guid id)
        {
            return Ok(await _packService.GeneratePack(id));
        }
    }
}
