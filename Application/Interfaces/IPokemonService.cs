using System;
using Domain.Entities;
using Domain.Enums;
using Domain.ViewModels;

namespace Application.Interfaces
{
	public interface IPokemonService
	{
		public Task<List<Pokemon>> GetPokemons(
			CancellationToken token = default);

        public Task<List<PokemonStatistics>> GeneratePokemonStatistics(
			SortByCol sortBy,
			SortDirection? sortDirection = SortDirection.descending,
			CancellationToken token = default);
    }
}

