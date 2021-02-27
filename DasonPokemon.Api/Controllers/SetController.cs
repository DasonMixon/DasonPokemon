using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DasonPokemon.Core.Entities;
using DasonPokemon.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DasonPokemon.Api.Controllers
{
    [ApiController]
    [Route("api/sets")]
    public class SetController : ControllerBase
    {
        private readonly ISetService _setService;

        public SetController(ISetService setService)
        {
            _setService = setService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Set>> GetSetById(Guid id)
        {
            var set = await _setService.GetAsync(id);
            return set == null ? NotFound() : Ok(set);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Set>>> GetAllSets()
        {
            var sets = await _setService.GetAllAsync();
            return sets.Any() ? Ok(sets) : NotFound();
        }


        [HttpGet("generatepack/{setId}")]
        public async Task<ActionResult<IEnumerable<Set>>> GenerateDeckFromSet(Guid setId)
        {
            var pack = await _setService.GeneratePack(setId);
            return NotFound();
        }
    }
}
