using System;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModels;
using Moq;

namespace _Test.Application
{
	public class PokemonServiceTest 
	{

        public PokemonServiceTest()
        {

        }

        [Fact]
		public async void GeneratePokemonStatistics_SortBy_Id_SortDirection_Ascending_Returns_List() {

            // arrange
            var pokemonRepository = new Mock<IPokemonRepository>(MockBehavior.Strict);

            var data = new List<Domain.Entities.Pokemon>
            {
                new Domain.Entities.Pokemon
                {
                    Id = 1,
                    Name = "pokemon_1",
                    base_experience = 100,
                    Types = new List<PokemonType>
                    {
                        new PokemonType
                        {
                            Slot = 1,
                            Type = new TypeProfile
                            {
                                Name = "fire"
                            }
                        }
                    }
                },
                new Domain.Entities.Pokemon
                {
                    Id = 2,
                    Name = "pokemon_2",
                    base_experience = 100,
                    Types = new List<PokemonType>
                    {
                        new PokemonType
                        {
                            Slot = 1,
                            Type = new TypeProfile
                            {
                                Name = "water"
                            }
                        }
                    }
                },
                new Domain.Entities.Pokemon
                {
                    Id = 3,
                    Name = "pokemon_3",
                    base_experience = 100,
                    Types = new List<PokemonType>
                    {
                        new PokemonType
                        {
                            Slot = 1,
                            Type = new TypeProfile
                            {
                                Name = "grass"
                            }
                        }
                    }
                }
            };

            pokemonRepository.Setup(p => p.GetPokemons(CancellationToken.None)).ReturnsAsync(data);

            var pokemonService = new PokemonService(pokemonRepository.Object);

            // act
            var response = await pokemonService.GeneratePokemonStatistics(
                Domain.Enums.SortByCol.Id,
                Domain.Enums.SortDirection.ascending,
                CancellationToken.None);

            // assert
            Assert.Equal(3, response.Count);
            Assert.IsType<List<PokemonStatistics>>(response);
            Assert.Equal(1, response.First(r => r.Id == 3).Ties);
            Assert.Equal(1, response.First(r => r.Id == 3).Loses);
            Assert.Equal(1, response.First(r => r.Id == 1).Wins);
            Assert.Equal(1, response.First(r => r.Id == 2).Wins);
            Assert.Equal(1, response.First().Id);
            Assert.Equal(3, response.Last().Id);

        }

        [Fact]
        public async void GeneratePokemonStatistics_SortBy_Name_SortDirection_Descending_Returns_List()
        {

            // arrange
            var pokemonRepository = new Mock<IPokemonRepository>(MockBehavior.Strict);

            var data = new List<Domain.Entities.Pokemon>
            {
                new Domain.Entities.Pokemon
                {
                    Id = 1,
                    Name = "pokemon_1",
                    base_experience = 100,
                    Types = new List<PokemonType>
                    {
                        new PokemonType
                        {
                            Slot = 1,
                            Type = new TypeProfile
                            {
                                Name = "fire"
                            }
                        }
                    }
                },
                new Domain.Entities.Pokemon
                {
                    Id = 2,
                    Name = "pokemon_2",
                    base_experience = 100,
                    Types = new List<PokemonType>
                    {
                        new PokemonType
                        {
                            Slot = 1,
                            Type = new TypeProfile
                            {
                                Name = "water"
                            }
                        }
                    }
                },
                new Domain.Entities.Pokemon
                {
                    Id = 3,
                    Name = "pokemon_3",
                    base_experience = 100,
                    Types = new List<PokemonType>
                    {
                        new PokemonType
                        {
                            Slot = 1,
                            Type = new TypeProfile
                            {
                                Name = "grass"
                            }
                        }
                    }
                }
            };

            pokemonRepository.Setup(p => p.GetPokemons(CancellationToken.None)).ReturnsAsync(data);

            var pokemonService = new PokemonService(pokemonRepository.Object);

            // act
            var response = await pokemonService.GeneratePokemonStatistics(
                Domain.Enums.SortByCol.Name,
                Domain.Enums.SortDirection.descending,
                CancellationToken.None);

            // assert
            Assert.Equal(3, response.Count);
            Assert.IsType<List<PokemonStatistics>>(response);
            Assert.Equal(1, response.First(r => r.Id == 3).Ties);
            Assert.Equal(1, response.First(r => r.Id == 3).Loses);
            Assert.Equal(1, response.First(r => r.Id == 1).Wins);
            Assert.Equal(1, response.First(r => r.Id == 2).Wins);
            Assert.Equal(3, response.First().Id);
            Assert.Equal(1, response.Last().Id);
        }

        [Fact]
        public async void GeneratePokemonStatistics_SortBy_Wins_SortDirection_Default_Returns_List()
        {

            // arrange
            var pokemonRepository = new Mock<IPokemonRepository>(MockBehavior.Strict);

            var data = new List<Domain.Entities.Pokemon>
            {
                new Domain.Entities.Pokemon
                {
                    Id = 1,
                    Name = "pokemon_1",
                    base_experience = 100,
                    Types = new List<PokemonType>
                    {
                        new PokemonType
                        {
                            Slot = 1,
                            Type = new TypeProfile
                            {
                                Name = "fire"
                            }
                        }
                    }
                },
                new Domain.Entities.Pokemon
                {
                    Id = 2,
                    Name = "pokemon_2",
                    base_experience = 100,
                    Types = new List<PokemonType>
                    {
                        new PokemonType
                        {
                            Slot = 1,
                            Type = new TypeProfile
                            {
                                Name = "water"
                            }
                        }
                    }
                },
                new Domain.Entities.Pokemon
                {
                    Id = 3,
                    Name = "pokemon_3",
                    base_experience = 100,
                    Types = new List<PokemonType>
                    {
                        new PokemonType
                        {
                            Slot = 1,
                            Type = new TypeProfile
                            {
                                Name = "grass"
                            }
                        }
                    }
                }
            };

            pokemonRepository.Setup(p => p.GetPokemons(CancellationToken.None)).ReturnsAsync(data);

            var pokemonService = new PokemonService(pokemonRepository.Object);

            // act
            var response = await pokemonService.GeneratePokemonStatistics(
                Domain.Enums.SortByCol.Wins,
                null,
                CancellationToken.None);

            // assert
            Assert.Equal(3, response.Count);
            Assert.IsType<List<PokemonStatistics>>(response);
            Assert.Equal(1, response.First(r => r.Id == 3).Ties);
            Assert.Equal(1, response.First(r => r.Id == 3).Loses);
            Assert.Equal(1, response.First(r => r.Id == 1).Wins);
            Assert.Equal(1, response.First(r => r.Id == 2).Wins);
            Assert.Equal(1, response.First().Id);
            Assert.Equal(3, response.Last().Id);
        }
    }
}

