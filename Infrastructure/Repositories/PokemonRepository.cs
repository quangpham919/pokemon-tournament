using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ViewModels;
using Newtonsoft.Json;

namespace Infrastructure.Repositories
{
	public class PokemonRepository : IPokemonRepository
	{
        private readonly HttpClient httpClient;
        private const string URL = "https://pokeapi.co/api/v2/pokemon/";
        private const int MAX_POKEMON = 8;

		public PokemonRepository()
		{
            this.httpClient = new HttpClient();
		}

        public async Task<IList<Pokemon>> GetPokemons(
            CancellationToken token = default)
        {
            var ids = GenRandomIds();

            var pokemons = new List<Pokemon>();

            foreach (var id in ids)
            {
                var apiResponse = await httpClient.GetAsync(URL + id, token);

                apiResponse.EnsureSuccessStatusCode();
                var dataAsString = await apiResponse.Content.ReadAsStringAsync(token);

                var parsedData = JsonConvert.DeserializeObject<Pokemon>(dataAsString);
                if (parsedData != null)
                {
                    pokemons.Add(parsedData);
                }
            }

            return pokemons;
        }

        private static List<int> GenRandomIds()
        {
            HashSet<int> randomIds = new HashSet<int>();
            var random = new Random();
            while (randomIds.Count < MAX_POKEMON) 
            {
                randomIds.Add(random.Next(1, 151));
            }
            return randomIds.ToList();
        }
    }
}

