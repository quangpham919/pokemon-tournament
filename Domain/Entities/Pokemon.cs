using System;
namespace Domain.Entities
{
	public class Pokemon
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int base_experience { get; set; }

		public IList<PokemonType> Types { get; set; }
	}
}

