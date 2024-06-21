using System;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ViewModels;

namespace Application.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository pokemonRepository;

        public PokemonService(IPokemonRepository pokemonRepository)
        {
            this.pokemonRepository = pokemonRepository;
        }

        public async Task<List<Pokemon>> GetPokemons(CancellationToken token = default)
        {
            var pokemons = await pokemonRepository.GetPokemons(token);

            return pokemons.ToList();
        }

        public async Task<List<PokemonStatistics>> GeneratePokemonStatistics(
            SortByCol sortBy,
            SortDirection? sortDirection = SortDirection.descending,
            CancellationToken token = default)
        {
            if (sortBy == null)
            {
                throw new ArgumentException("sortBy is required");
            }

            if (sortDirection == null)
            {
                sortDirection = SortDirection.descending;
            }

            var pokemons = await pokemonRepository.GetPokemons(token);

            var pokemonStatistics = RunTournament(pokemons.ToList());

            var sortByCol = Enum.GetName(typeof(SortByCol), sortBy);

            pokemonStatistics = sortDirection == SortDirection.descending
                ? OrderByPropertyDesc(pokemonStatistics.AsEnumerable(), sortByCol).ToList()
                : OrderByProperty(pokemonStatistics.AsEnumerable(), sortByCol).ToList();

            return await Task.FromResult<List<PokemonStatistics>>(pokemonStatistics);
        }

        private static List<PokemonStatistics> RunTournament(List<Pokemon> pokemons)
        {
            var statistics = pokemons.Select(p => new PokemonStatistics
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Types.First(t => t.Slot == 1).Type.Name,
                Wins = 0,
                Loses = 0,
                Ties = 0
            }).ToList();

            // Compare each pokemon with the rest in the list using two pointers method 
            // To makse sure each pair will be visited only once.
            // As we have 8 total, there are 28 pairs of pokemon.
            for (var pointer_1 = 0; pointer_1 < pokemons.Count() - 1; pointer_1++)
            {
                var currentPoke = pokemons[pointer_1];
                var currentPokeFirstType = currentPoke.Types.First(t => t.Slot == 1).Type.Name;
                var currPokeHasType = Enum.TryParse<PokemonTypeEnum>(
                    currentPokeFirstType,
                    true,
                    out PokemonTypeEnum currType);

                for (var pointer_2 = pointer_1 + 1; pointer_2 < pokemons.Count(); pointer_2++)
                {
                    var otherPoke = pokemons[pointer_2];
                    var otherPokeFirstType = otherPoke.Types.First(t => t.Slot == 1).Type.Name;
                    var otherPokeHasType = Enum.TryParse<PokemonTypeEnum>(
                        otherPokeFirstType,
                        true,
                        out PokemonTypeEnum otherType);

                    var fightingResult = 0;

                    if (currPokeHasType && otherPokeHasType)
                    {
                        fightingResult = CompareType(currType, otherType);
                    }

                    if (fightingResult == 0)
                    {
                        fightingResult = currentPoke.base_experience.CompareTo(otherPoke.base_experience);
                    }

                    var currentPokeStat = statistics[pointer_1];
                    var otherPokeStat = statistics[pointer_2];

                    if (fightingResult >= 1)
                    {
                        currentPokeStat.Wins += 1;
                        otherPokeStat.Loses += 1;
                    }
                    else if (fightingResult <= -1)
                    {
                        currentPokeStat.Loses += 1;
                        otherPokeStat.Wins += 1;
                    }
                    else
                    {
                        currentPokeStat.Ties += 1;
                        otherPokeStat.Ties += 1;
                    }
                }
            }

            return statistics;
        }

        private static int CompareType(PokemonTypeEnum currType, PokemonTypeEnum otherType)
        {
            switch (currType)
            {
                case PokemonTypeEnum.Water:
                    if (otherType == PokemonTypeEnum.Fire)
                    {
                        return 1;
                    }
                    else if (otherType == PokemonTypeEnum.Electric)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }

                case PokemonTypeEnum.Fire:
                    if (otherType == PokemonTypeEnum.Grass)
                    {
                        return 1;
                    }
                    else if (otherType == PokemonTypeEnum.Water)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }

                case PokemonTypeEnum.Grass:
                    if (otherType == PokemonTypeEnum.Electric)
                    {
                        return 1;
                    }
                    else if (otherType == PokemonTypeEnum.Fire)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }

                case PokemonTypeEnum.Electric:
                    if (otherType == PokemonTypeEnum.Water)
                    {
                        return 1;
                    }
                    else if (otherType == PokemonTypeEnum.Grass)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }

                case PokemonTypeEnum.Ghost:
                    if (otherType == PokemonTypeEnum.Psychic)
                    {
                        return 1;
                    }
                    else if (otherType == PokemonTypeEnum.Dark)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }

                case PokemonTypeEnum.Psychic:
                    if (otherType == PokemonTypeEnum.Fighting)
                    {
                        return 1;
                    }
                    else if (otherType == PokemonTypeEnum.Ghost)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }

                case PokemonTypeEnum.Fighting:
                    if (otherType == PokemonTypeEnum.Dark)
                    {
                        return 1;
                    }
                    else if (otherType == PokemonTypeEnum.Psychic)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }

                case PokemonTypeEnum.Dark:
                    if (otherType == PokemonTypeEnum.Ghost)
                    {
                        return 1;
                    }
                    else if (otherType == PokemonTypeEnum.Fighting)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                default:
                    return 0;
            }
        }

        private static IEnumerable<T> OrderByProperty<T>(IEnumerable<T> source, string propertyName)
        {
            return source.OrderBy(x => typeof(T).GetProperty(propertyName).GetValue(x));
        }

        private static IEnumerable<T> OrderByPropertyDesc<T>(IEnumerable<T> source, string propertyName)
        {
            return source.OrderByDescending(x => typeof(T).GetProperty(propertyName).GetValue(x));
        }
    }
}

