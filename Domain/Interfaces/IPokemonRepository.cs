using System;
using Domain.Entities;
using Domain.Enums;
using Domain.ViewModels;

namespace Domain.Interfaces
{
	public interface IPokemonRepository
	{
		public Task<IList<Pokemon>> GetPokemons(
			CancellationToken token = default);
	}
}

