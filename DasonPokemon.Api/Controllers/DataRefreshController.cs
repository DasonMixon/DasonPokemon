using System.Threading.Tasks;
using DasonPokemon.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DasonPokemon.Api.Controllers
{
    [ApiController]
    [Route("api/datarefresh")]
    public class DataRefreshController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDataRefreshService _dataRefreshService;

        public DataRefreshController(IConfiguration configuration, IDataRefreshService dataRefreshService)
        {
            _configuration = configuration;
            _dataRefreshService = dataRefreshService;
        }

        [HttpGet("{apiKey}")]
        public async Task<IActionResult> Refresh(string apiKey)
        {
            var expectedApiKey = _configuration.GetValue<string>("pokemontcg.apikey");
            if (!expectedApiKey.Equals(apiKey, System.StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest("Invalid API key");
            }

            var baseUrl = _configuration.GetValue<string>("pokemontcg.baseUrl");
            var success = await _dataRefreshService.Refresh(baseUrl, apiKey);
            return success ? Ok() : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
