using System;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Pokemen.Controllers
{
    [ApiController]
    [Route("[controller]")]
	public class PokemonController : ControllerBase
	{
		private readonly IPokemonService pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }

        [HttpGet("tournament/statistics")]
        public async Task<IActionResult> GetTournamentStatistics(
            [FromQuery] SortByCol? sortBy,
            [FromQuery] SortDirection? sortDirection = SortDirection.descending,
            CancellationToken token = default)
        {
            if (sortBy == null)
            {
                return BadRequest("sortBy is required");
            };

            var response = await pokemonService.GeneratePokemonStatistics((SortByCol) sortBy, sortDirection, token);

            return Ok(response);
        }
	}
}

