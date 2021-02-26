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
    [Route("api/cards")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        // TODO: Add logging everywhere

        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCardById(Guid id)
        {
            var card = await _cardService.GetAsync(id);
            return card == null ? NotFound() : Ok(card);
        }

        [HttpGet("set/{setId}")]
        public async Task<ActionResult<IEnumerable<Card>>> GetCardsBySetId(Guid setId)
        {
            var cards = await _cardService.GetAllFromSetAsync(setId);
            return cards.Any() ? Ok(cards) : NotFound();
        }
    }
}
