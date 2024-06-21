using System;
namespace Domain.ViewModels
{
	public class PokemonStatistics
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Type { get; set; }

		public int Wins { get; set; }

		public int Loses { get; set; }

		public int Ties { get; set; }
	}	
}

